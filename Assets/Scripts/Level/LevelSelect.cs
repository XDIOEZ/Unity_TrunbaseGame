using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    // Singleton Instance
    private static LevelSelect instance;
    public static LevelSelect Instance { get { return instance; } }

    // Dictionary to store levels
    public readonly Dictionary<int, Level> LevelsDictionary = new Dictionary<int, Level>();

    // Asset label reference for Addressables
    [SerializeField] private AssetLabelReference assetLabelReference;

    // Buttons and text for Level selection
    public Button[] buttons;
    public TextMeshProUGUI[] text;
    public Level[] levels;

    public Enemy enemy; // Assuming Enemy is a class or scriptable object

    void Awake()
    {
        // Ensure there is only one Instance of LevelSelect
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        LoadLevels();
        SetupButtonListeners();
        StartGame.OnGameStart += SetLevel;
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Local copy of the loop variable
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index)
    {
        Debug.Log($"Button {index} clicked.");
        LoadLevel(index);
    }

    public void LoadLevel(int ButtonIndex)
    {
        enemy.characterData = levels[ButtonIndex].Level_Battlecharacter[0];
    }

    private void Update()
    {
        // 将关卡名字显示到文本元素上
        for (int i = 0; i < levels.Length && i < text.Length; i++)
        {
            if (levels[i] != null && text[i] != null)
            {
                text[i].text = levels[i].level_Name; // 假设 Level 类有一个 level_Name 属性
            }
        }
    }

    public void SetLevel()
    {
        // 如果 LevelsDictionary 中没有关卡，直接返回
        if (LevelsDictionary.Count == 0)
        {
            Debug.LogWarning("LevelsDictionary is empty.");
            return;
        }

        // 清空 levels 数组，重新分配关卡
        levels = new Level[text.Length]; // 假设 text 数组的长度决定了 levels 数组的大小

        // 随机分配关卡到 levels 数组中
        List<int> usedIndexes = new List<int>(); // 用于记录已经使用过的索引
        System.Random random = new System.Random();

        for (int i = 0; i < levels.Length; i++)
        {
            // 随机选择一个未使用的索引
            int randomIndex = random.Next(0, LevelsDictionary.Count);

            // 确保不重复使用同一个索引
            while (usedIndexes.Contains(randomIndex))
            {
                randomIndex = random.Next(0, LevelsDictionary.Count);
            }

            // 将选中的关卡放入 levels 数组中
            levels[i] = LevelsDictionary[randomIndex];

            // 将使用过的索引记录下来
            usedIndexes.Add(randomIndex);
        }
    }

    private void LoadLevels()
    {
        if (assetLabelReference == null)
        {
            Debug.LogError("AssetLabelReference is not set.");
            return;
        }

        // Asynchronously load Prefabs using Addressables
        Addressables.LoadAssetsAsync<Level>(assetLabelReference, null).Completed += (AsyncOperationHandle<IList<Level>> operationHandle) =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var level in operationHandle.Result)
                {
                    if (level != null)
                    {
                        LevelsDictionary[level.id] = level;
                    }
                    else
                    {
                        Debug.LogWarning("Loaded Level is null.");
                    }
                }
                // 设置关卡名字
                levels = new Level[operationHandle.Result.Count];
                operationHandle.Result.CopyTo(levels, 0);
            }
            else
            {
                Debug.LogError("Failed to load levels.");
            }
        };
    }
}
