using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Map : MonoBehaviour
{
    // ��������
    public static Map Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject); // ��������Ѿ����ڣ������ٶ����ʵ��
        }
    }

    int seed;//��ͼ��˭��������

    public List<UI_Level> _LevelAtMap;//UI�ؿ�
    public List<string> ui_LevelName;//UI�ؿ����ֵ�����

    //ʹ��һά���鷽�����л�
    int width; //��ͼ���
    int height; //��ͼ�߶�


    // ��ͼ��Ⱥ͸߶�
    private const int MapWidth = 3;
    private const int MapHeight = 12;

    // ScriptableObject���ֵ䣬ӳ��ؿ����ֵ��ؿ�����
    public readonly Dictionary<int, Level> LevelsDictionary = new Dictionary<int, Level>();

    // �����ͼ���ݵĶ�ά����
    private int[,] mapData = new int[MapWidth, MapHeight];
    public int[] mapData_1D; // һά���飬���ڴ洢��ͼ����

    // ��������UI_Level���������
    public List<UI_levelData> uiLevelComponents_Data = new List<UI_levelData>();
    public List<UI_Level> uiLevelComponents = new List<UI_Level>();

    // ���浱ǰisHere״̬��UI_Level��λ��
    public Vector2Int? currentPosition = null;

    private Level currentLevel; // ��ǰ�ؿ�

    [SerializeField] public AssetLabelReference assetLabelReference; // Addressables�ı�ǩ����

    private void LoadLevels() // ��������еĹؿ�������
    {
        if (assetLabelReference == null)
        {
            Debug.LogError("AssetLabelReference is not set."); // �����ǩ����δ���ã����������Ϣ
            return;
        }

        // �첽����Level���͵�Prefabs
        Addressables.LoadAssetsAsync<Level>(assetLabelReference, null).Completed += (AsyncOperationHandle<IList<Level>> operationHandle) =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var level in operationHandle.Result)
                {
                    if (level != null)
                    {
                        LevelsDictionary[level.id] = level; // �����صĹؿ����ݴ洢���ֵ���
                    }
                    else
                    {
                        Debug.LogWarning("Loaded Level is null."); // ������صĹؿ�Ϊnull�����������Ϣ
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to load levels."); // �������ʧ�ܣ����������Ϣ
            }
        };
    }

    void Start()
    {
        InitializeUILevelComponents(); 
        LoadLevels(); // �Ӵ����м��عؿ�����
        StartGame.OnGameStart += InitializeMapAndInstantiateLevels; // ����һ���µ�ͼ
    }

    public void InitializeUILevelComponents() // ��ȡ��ͼ�е�����UI_Level�������data�����浽�б���
    {
        // ��յ�ǰ�� uiLevelComponents �б��Է�ֹ�ظ����
        uiLevelComponents.Clear();
        uiLevelComponents_Data.Clear();

        // ���������Ӷ���
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i); // ��ȡ�Ӷ���� Transform
            UI_Level uiLevelComponent = childTransform.GetComponent<UI_Level>(); // ��ȡ�Ӷ����ϵ� UI_Level ���

            if (uiLevelComponent != null)
            {
                uiLevelComponents.Add(uiLevelComponent); // �������ӵ� uiLevelComponents �б���
                uiLevelComponents_Data.Add(uiLevelComponent._data); // �� UI_Level ����� _data ������ӵ� uiLevelComponents_Data �б���
            }
            else
            {
                Debug.LogWarning("No UI_Level component found on: " + childTransform.name); // ���û���ҵ� UI_Level ������������
            }
        }
    }

    public void InitializeMapAndInstantiateLevels() // ��ʼһ���µ���Ϸ
    {
        mapData_1D = InitializeMapData(GameSettings.instance.seed); // ��ʼ����ͼ����
        InstantiateMapLevels(mapData_1D); // ʵ�����ؿ�Ԥ����
        OnFirstEnterMap(); // ��һ�ν����ͼʱ���ó�ʼ״̬
        currentPosition = new Vector2Int(-1, -1); // ��ʼ����ǰλ��
    }

    public void ContinueGame(Vector2Int playerPosition, List<UI_levelData> Save_uiLevelComponents) // ���������Ϸ,������¼�
    {
        OnContinueEnterMap(Save_uiLevelComponents);
        currentPosition = playerPosition; // ��ʼ����ǰλ��
    }
    int[] InitializeMapData(int seed) // ���ݴ�����������һ������Ķ�ά����,������ת��Ϊһά���鷵��
    {
        Debug.Log("InitializeMapData");

        // ��ȡ�ֵ��б���Ĺؿ�����
        int levelCount = LevelsDictionary.Count;

        // ȷ���ֵ������㹻�Ĺؿ�����
        if (levelCount == 0)
        {
            Debug.LogError("LevelsDictionary is empty. Cannot initialize map data.");
            return new int[0];
        }

        // ʹ��������ӳ�ʼ�������������
        Random.InitState(seed);

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                // ������������1���ֵ��йؿ�����֮����������������ͼ����
                mapData[x, y] = Random.Range(0, levelCount);
            }
        }

        // ����ά������ϵ���ת��Ϊһά����
        mapData_1D = new int[MapWidth * MapHeight];
        for (int i = 0; i < MapWidth * MapHeight; i++)
        {
            mapData_1D[i] = mapData[i / MapHeight, i % MapHeight];
        }

        return mapData_1D;
    }

    void InstantiateMapLevels(int[] D_mapData) // ���ݴ����һά����,�����ֵ������Ѿ�ʵ�����Ĺؿ�Ԥ���������
    {
        Transform parentTransform = this.transform; // ��ȡ��ǰ����� Transform

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i); // ��ȡ�Ӷ���� Transform
            UI_Level uiLevelComponent = childTransform.GetComponent<UI_Level>(); // ��ȡ UI_Level ���

            if (uiLevelComponent != null)
            {
                int levelID;

                // ����Ƿ�����Ͳ�Ĺؿ�
                if (i >= (MapHeight - 1) * MapWidth && i < MapHeight * MapWidth)
                {
                    levelID = 4; // ����Ͳ�Ĺؿ�ID�̶�Ϊ4
                }
                else
                {
                    levelID = D_mapData[i]; // ʹ��ԭʼ�Ĺؿ�ID
                }

                uiLevelComponent.Level = LevelsDictionary[levelID]; // ���ؿ����ݷ���� UI_Level ���
                uiLevelComponent.SetLevelImage(uiLevelComponent.Level.Level_sprite); // ���� UI_Level ��ͼƬ
                uiLevelComponent.Position = new Vector2Int(i % MapWidth, i / MapWidth); // ���� UI_Level ��λ��

               
            }
            else
            {
                Debug.LogWarning("No UI_Level component found on: " + childTransform.name); // ���δ�ҵ� UI_Level ��������������Ϣ
            }
        }
    }
    public void UpdateIsHereStatus(Vector2Int newPosition) // ���ǰ�������״̬
    {
        // ��������λ�ú� currentPosition ��һ��
        if (currentPosition == null || currentPosition.Value != newPosition)
        {
            // �����������һ�㣨�жϱ�׼��y ����仯��
            if (currentPosition != null && currentPosition.Value.y != newPosition.y)
            {
                int previousLayer = currentPosition.Value.y;
                int newLayer = newPosition.y;

                // ���²������ؿ��� CanMove ��������Ϊ false
                for (int x = 0; x < MapWidth; x++)
                {
                    int index = newLayer * MapWidth + x;
                    if (index != newPosition.y * MapWidth + newPosition.x) // ���Ե�ǰ�µ�λ��
                    {
                        uiLevelComponents[index].SetCanMove(false);
                    }
                }

                // ����һ�㣨��һ�㣩������ CanMove ��������Ϊ true
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

            // ���֮ǰ��һ��λ�ã����� IsHere ������Ϊ false
            if (currentPosition != new Vector2Int(-1, -1)) // ʹ�� Vector2Int ����
            {
                int oldIndex = currentPosition.Value.y * MapWidth + currentPosition.Value.x;
                uiLevelComponents[oldIndex].IsHere = false;
            }

            // ���� currentPosition
            currentPosition = newPosition;

            // �����µ�λ�õ� IsHere ����Ϊ true
            int newIndex = newPosition.y * MapWidth + newPosition.x;
            uiLevelComponents[newIndex].IsHere = true;

            Debug.Log($"Updated currentPosition to: ({newPosition.x}, {newPosition.y})");
            SaveGame.Instance.PlayerSave.Save_PlayerPosition = currentPosition.Value; // �������λ��
        }
    }

    public void OnFirstEnterMap() // ����ײ��3���ؿ�����Ϊ���ƶ��Ľ����ؿ�
    {
        int startRow = MapHeight - 1; // ��ͼ��ײ�һ��
        for (int x = 0; x < MapWidth; x++)
        {
            Transform childTransform = this.transform.GetChild(x + startRow * MapWidth); // ��ȡ�ײ���Ӷ���
            UI_Level uiLevelComponent = childTransform.GetComponent<UI_Level>(); // ��ȡ UI_Level ���

            if (uiLevelComponent != null)
            {
                uiLevelComponent.SetCanMove(true); // ���õײ�� UI_Level �����ƶ�
            }
            else
            {
                Debug.LogWarning($"No UI_Level component found on the bottom row at index: {x}"); // ���δ�ҵ� UI_Level ��������������Ϣ
            }
        }
    }

    public void OnContinueEnterMap(List<UI_levelData> Save_uiLevelComponents) // �����õ� List<UI_Level> �����Ը�ֵ����ǰ�� List<UI_Level>
    {
        uiLevelComponents_Data  = Save_uiLevelComponents; // ��ֵ��ǰ�� List<UI_Level> ������

       for (int i = 0; i < uiLevelComponents.Count; i++)
       {
            uiLevelComponents[i]._data = uiLevelComponents_Data[i]; // ��ֵ��ǰ�� List<UI_Level> ������
            uiLevelComponents[i].SetLevelImage(uiLevelComponents[i].Level.Level_sprite);
           
        }
        
    }
}
