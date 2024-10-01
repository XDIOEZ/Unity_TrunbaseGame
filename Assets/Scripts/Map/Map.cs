using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Map : MonoBehaviour
{
    // 创建单例
    public static Map Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject); // 如果单例已经存在，则销毁多余的实例
        }
    }

    int seed;//地图的谁技术种子

    public List<UI_Level> _LevelAtMap;//UI关卡
    public List<string> ui_LevelName;//UI关卡名字的排列

    //使用一维数组方便序列化
    int width; //地图宽度
    int height; //地图高度


    // 地图宽度和高度
    private const int MapWidth = 3;
    private const int MapHeight = 12;

    // ScriptableObject的字典，映射关卡数字到关卡数据
    public readonly Dictionary<int, Level> LevelsDictionary = new Dictionary<int, Level>();

    // 保存地图数据的二维数组
    private int[,] mapData = new int[MapWidth, MapHeight];
    public int[] mapData_1D; // 一维数组，用于存储地图数据

    // 保存所有UI_Level组件的引用
    public List<UI_levelData> uiLevelComponents_Data = new List<UI_levelData>();
    public List<UI_Level> uiLevelComponents = new List<UI_Level>();

    // 保存当前isHere状态的UI_Level的位置
    public Vector2Int? currentPosition = null;

    private Level currentLevel; // 当前关卡

    [SerializeField] public AssetLabelReference assetLabelReference; // Addressables的标签引用

    private void LoadLevels() // 载入磁盘中的关卡的数据
    {
        if (assetLabelReference == null)
        {
            Debug.LogError("AssetLabelReference is not set."); // 如果标签引用未设置，输出错误信息
            return;
        }

        // 异步加载Level类型的Prefabs
        Addressables.LoadAssetsAsync<Level>(assetLabelReference, null).Completed += (AsyncOperationHandle<IList<Level>> operationHandle) =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var level in operationHandle.Result)
                {
                    if (level != null)
                    {
                        LevelsDictionary[level.id] = level; // 将加载的关卡数据存储到字典中
                    }
                    else
                    {
                        Debug.LogWarning("Loaded Level is null."); // 如果加载的关卡为null，输出警告信息
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to load levels."); // 如果加载失败，输出错误信息
            }
        };
    }

    void Start()
    {
        InitializeUILevelComponents(); 
        LoadLevels(); // 从磁盘中加载关卡数据
        StartGame.OnGameStart += InitializeMapAndInstantiateLevels; // 创建一个新地图
    }

    public void InitializeUILevelComponents() // 获取地图中的所有UI_Level组件和其data并保存到列表中
    {
        // 清空当前的 uiLevelComponents 列表，以防止重复添加
        uiLevelComponents.Clear();
        uiLevelComponents_Data.Clear();

        // 遍历所有子对象
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i); // 获取子对象的 Transform
            UI_Level uiLevelComponent = childTransform.GetComponent<UI_Level>(); // 获取子对象上的 UI_Level 组件

            if (uiLevelComponent != null)
            {
                uiLevelComponents.Add(uiLevelComponent); // 将组件添加到 uiLevelComponents 列表中
                uiLevelComponents_Data.Add(uiLevelComponent._data); // 将 UI_Level 组件的 _data 属性添加到 uiLevelComponents_Data 列表中
            }
            else
            {
                Debug.LogWarning("No UI_Level component found on: " + childTransform.name); // 如果没有找到 UI_Level 组件，输出警告
            }
        }
    }

    public void InitializeMapAndInstantiateLevels() // 开始一个新的游戏
    {
        mapData_1D = InitializeMapData(GameSettings.instance.seed); // 初始化地图数据
        InstantiateMapLevels(mapData_1D); // 实例化关卡预制体
        OnFirstEnterMap(); // 第一次进入地图时设置初始状态
        currentPosition = new Vector2Int(-1, -1); // 初始化当前位置
    }

    public void ContinueGame(Vector2Int playerPosition, List<UI_levelData> Save_uiLevelComponents) // 点击继续游戏,处理的事件
    {
        OnContinueEnterMap(Save_uiLevelComponents);
        currentPosition = playerPosition; // 初始化当前位置
    }
    int[] InitializeMapData(int seed) // 根据传入种子生成一个随机的二维数组,并将其转换为一维数组返回
    {
        Debug.Log("InitializeMapData");

        // 获取字典中保存的关卡数量
        int levelCount = LevelsDictionary.Count;

        // 确保字典中有足够的关卡数据
        if (levelCount == 0)
        {
            Debug.LogError("LevelsDictionary is empty. Cannot initialize map data.");
            return new int[0];
        }

        // 使用随机种子初始化随机数生成器
        Random.InitState(seed);

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                // 根据种子生成1到字典中关卡数量之间的随机数，并填充地图数据
                mapData[x, y] = Random.Range(0, levelCount);
            }
        }

        // 将二维数组从上到下转换为一维数组
        mapData_1D = new int[MapWidth * MapHeight];
        for (int i = 0; i < MapWidth * MapHeight; i++)
        {
            mapData_1D[i] = mapData[i / MapHeight, i % MapHeight];
        }

        return mapData_1D;
    }

    void InstantiateMapLevels(int[] D_mapData) // 根据传入的一维数组,根据字典设置已经实例化的关卡预制体的属性
    {
        Transform parentTransform = this.transform; // 获取当前对象的 Transform

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i); // 获取子对象的 Transform
            UI_Level uiLevelComponent = childTransform.GetComponent<UI_Level>(); // 获取 UI_Level 组件

            if (uiLevelComponent != null)
            {
                int levelID;

                // 检查是否是最低层的关卡
                if (i >= (MapHeight - 1) * MapWidth && i < MapHeight * MapWidth)
                {
                    levelID = 4; // 将最低层的关卡ID固定为4
                }
                else
                {
                    levelID = D_mapData[i]; // 使用原始的关卡ID
                }

                uiLevelComponent.Level = LevelsDictionary[levelID]; // 将关卡数据分配给 UI_Level 组件
                uiLevelComponent.SetLevelImage(uiLevelComponent.Level.Level_sprite); // 设置 UI_Level 的图片
                uiLevelComponent.Position = new Vector2Int(i % MapWidth, i / MapWidth); // 设置 UI_Level 的位置

               
            }
            else
            {
                Debug.LogWarning("No UI_Level component found on: " + childTransform.name); // 如果未找到 UI_Level 组件，输出警告信息
            }
        }
    }
    public void UpdateIsHereStatus(Vector2Int newPosition) // 玩家前进后更新状态
    {
        // 如果传入的位置和 currentPosition 不一样
        if (currentPosition == null || currentPosition.Value != newPosition)
        {
            // 如果进入了新一层（判断标准：y 坐标变化）
            if (currentPosition != null && currentPosition.Value.y != newPosition.y)
            {
                int previousLayer = currentPosition.Value.y;
                int newLayer = newPosition.y;

                // 将新层其他关卡的 CanMove 属性设置为 false
                for (int x = 0; x < MapWidth; x++)
                {
                    int index = newLayer * MapWidth + x;
                    if (index != newPosition.y * MapWidth + newPosition.x) // 忽略当前新的位置
                    {
                        uiLevelComponents[index].SetCanMove(false);
                    }
                }

                // 将下一层（上一层）的所有 CanMove 属性设置为 true
                int nextLayer = newLayer - 1;
                if (nextLayer >= 0)
                {
                    for (int x = 0; x < MapWidth; x++)
                    {
                        int index = nextLayer * MapWidth + x;
                        uiLevelComponents[index].SetCanMove(true);
                    }
                }

               
            }

            // 如果之前有一个位置，将其 IsHere 属性设为 false
            if (currentPosition != new Vector2Int(-1, -1)) // 使用 Vector2Int 处理
            {
                int oldIndex = currentPosition.Value.y * MapWidth + currentPosition.Value.x;
                uiLevelComponents[oldIndex].IsHere = false;
            }

            // 更新 currentPosition
            currentPosition = newPosition;

            // 设置新的位置的 IsHere 属性为 true
            int newIndex = newPosition.y * MapWidth + newPosition.x;
            uiLevelComponents[newIndex].IsHere = true;

            Debug.Log($"Updated currentPosition to: ({newPosition.x}, {newPosition.y})");
            SaveGame.Instance.PlayerSave.Save_PlayerPosition = currentPosition.Value; // 保存玩家位置
        }
    }

    public void OnFirstEnterMap() // 将最底层的3个关卡设置为可移动的奖励关卡
    {
        int startRow = MapHeight - 1; // 地图最底层一行
        for (int x = 0; x < MapWidth; x++)
        {
            Transform childTransform = this.transform.GetChild(x + startRow * MapWidth); // 获取底层的子对象
            UI_Level uiLevelComponent = childTransform.GetComponent<UI_Level>(); // 获取 UI_Level 组件

            if (uiLevelComponent != null)
            {
                uiLevelComponent.SetCanMove(true); // 设置底层的 UI_Level 可以移动
            }
            else
            {
                Debug.LogWarning($"No UI_Level component found on the bottom row at index: {x}"); // 如果未找到 UI_Level 组件，输出警告信息
            }
        }
    }

    public void OnContinueEnterMap(List<UI_levelData> Save_uiLevelComponents) // 将调用的 List<UI_Level> 的属性赋值给当前的 List<UI_Level>
    {
        uiLevelComponents_Data  = Save_uiLevelComponents; // 赋值当前的 List<UI_Level> 的属性

       for (int i = 0; i < uiLevelComponents.Count; i++)
       {
            uiLevelComponents[i]._data = uiLevelComponents_Data[i]; // 赋值当前的 List<UI_Level> 的属性
            uiLevelComponents[i].SetLevelImage(uiLevelComponents[i].Level.Level_sprite);
           
        }
        
    }
}
