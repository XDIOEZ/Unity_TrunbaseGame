using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PlayerSkillManager : MonoBehaviour
{
    #region 单例模式
 public static PlayerSkillManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
   
    public void AddSkill(Skill skill) // Add skill to inventory
    {
        SaveGame.Instance.PlayerSave.Save_PlayerSkillsBag.Add(skill);
    }
    public void RemoveSkill(Skill skill) //remove skill from inventory
    {

        SaveGame.Instance.PlayerSave.Save_PlayerSkillsBag.Remove(skill);
    }

 

    // Dictionary to store skills
    private readonly Dictionary<int, Skill> SkillsDictionary = new Dictionary<int, Skill>();

    // Asset label reference for Addressables
    [SerializeField] private AssetLabelReference assetLabelReference;

    // Buttons and text for skill selection
    public Button[] buttons;
    public TextMeshProUGUI[] text;
    public Skill[] skills;

   public GameObject[] gameObjects;//todo 我在此提供gameobject，我希望创建一个方法自动获取其中的buttons和text赋值到上面的数组中去

    void Start()
    {
        InitializeArrays();
        LoadSkills();
        SetupButtonListeners();
     
    }

    private void InitializeArrays()
    {
        // Initialize the arrays
        buttons = new Button[gameObjects.Length];
        text = new TextMeshProUGUI[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            buttons[i] = gameObjects[i].GetComponentInChildren<Button>();
            text[i] = gameObjects[i].GetComponentInChildren<TextMeshProUGUI>();

            if (buttons[i] == null)
            {
                Debug.LogWarning($"No Button component found in {gameObjects[i].name}");
            }

            if (text[i] == null)
            {
                Debug.LogWarning($"No TextMeshProUGUI component found in {gameObjects[i].name}");
            }
        }
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
         AddSkill(skills[index]);
    }
    private void Update()
    {
        // 将技能名字显示到文本元素上
        for (int i = 0; i < skills.Length && i < text.Length; i++)
        {
            if (skills[i] != null && text[i] != null)
            {
                text[i].text = skills[i].skillName;
            }
        }
    }
    public void SetSkills()
    {
        // 如果 SkillsDictionary 中没有技能，直接返回
        if (SkillsDictionary.Count == 0)
        {
            Debug.LogWarning("SkillsDictionary is empty.");
            return;
        }

        // 清空 skills 数组，重新分配技能
        skills = new Skill[text.Length]; // 假设 text 数组的长度决定了 skills 数组的大小

        // 随机分配技能到 skills 数组中
        List<int> usedIndexes = new List<int>(); // 用于记录已经使用过的索引
        System.Random random = new System.Random();

        for (int i = 0; i < skills.Length; i++)
        {
            // 随机选择一个未使用的索引
            int randomIndex = random.Next(0, SkillsDictionary.Count);

            // 确保不重复使用同一个索引
            while (usedIndexes.Contains(randomIndex))
            {
                randomIndex = random.Next(0, SkillsDictionary.Count);
            }

            // 将选中的技能放入 skills 数组中
            skills[i] = SkillsDictionary[randomIndex];

            // 将使用过的索引记录下来
            usedIndexes.Add(randomIndex);
        }
    }
    private void LoadSkills()
    {
        if (assetLabelReference == null)
        {
            Debug.LogError("AssetLabelReference is not set.");
            return;
        }

        // Asynchronously load skills using Addressables
        Addressables.LoadAssetsAsync<Skill>(assetLabelReference, null).Completed += (AsyncOperationHandle<IList<Skill>> operationHandle) =>
        {
            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var skill in operationHandle.Result)
                {
                    if (skill != null)
                    {
                        SkillsDictionary[skill.skillID] = skill;
                    }
                    else
                    {
                        Debug.LogWarning("Loaded skill is null.");
                    }
                }
                // 设置技能
                skills = new Skill[operationHandle.Result.Count];
                operationHandle.Result.CopyTo(skills, 0);
            }
            else
            {
                Debug.LogError("Failed to load skills.");
            }
        };
    }
}
