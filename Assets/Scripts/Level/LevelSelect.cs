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
        // ���ؿ�������ʾ���ı�Ԫ����
        for (int i = 0; i < levels.Length && i < text.Length; i++)
        {
            if (levels[i] != null && text[i] != null)
            {
                text[i].text = levels[i].level_Name; // ���� Level ����һ�� level_Name ����
            }
        }
    }

    public void SetLevel()
    {
        // ��� LevelsDictionary ��û�йؿ���ֱ�ӷ���
        if (LevelsDictionary.Count == 0)
        {
            Debug.LogWarning("LevelsDictionary is empty.");
            return;
        }

        // ��� levels ���飬���·���ؿ�
        levels = new Level[text.Length]; // ���� text ����ĳ��Ⱦ����� levels ����Ĵ�С

        // �������ؿ��� levels ������
        List<int> usedIndexes = new List<int>(); // ���ڼ�¼�Ѿ�ʹ�ù�������
        System.Random random = new System.Random();

        for (int i = 0; i < levels.Length; i++)
        {
            // ���ѡ��һ��δʹ�õ�����
            int randomIndex = random.Next(0, LevelsDictionary.Count);

            // ȷ�����ظ�ʹ��ͬһ������
            while (usedIndexes.Contains(randomIndex))
            {
                randomIndex = random.Next(0, LevelsDictionary.Count);
            }

            // ��ѡ�еĹؿ����� levels ������
            levels[i] = LevelsDictionary[randomIndex];

            // ��ʹ�ù���������¼����
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
                // ���ùؿ�����
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
