using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PlayerSkillManager : MonoBehaviour
{
    #region ����ģʽ
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

   public GameObject[] gameObjects;//todo ���ڴ��ṩgameobject����ϣ������һ�������Զ���ȡ���е�buttons��text��ֵ�������������ȥ

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
        // ������������ʾ���ı�Ԫ����
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
        // ��� SkillsDictionary ��û�м��ܣ�ֱ�ӷ���
        if (SkillsDictionary.Count == 0)
        {
            Debug.LogWarning("SkillsDictionary is empty.");
            return;
        }

        // ��� skills ���飬���·��似��
        skills = new Skill[text.Length]; // ���� text ����ĳ��Ⱦ����� skills ����Ĵ�С

        // ������似�ܵ� skills ������
        List<int> usedIndexes = new List<int>(); // ���ڼ�¼�Ѿ�ʹ�ù�������
        System.Random random = new System.Random();

        for (int i = 0; i < skills.Length; i++)
        {
            // ���ѡ��һ��δʹ�õ�����
            int randomIndex = random.Next(0, SkillsDictionary.Count);

            // ȷ�����ظ�ʹ��ͬһ������
            while (usedIndexes.Contains(randomIndex))
            {
                randomIndex = random.Next(0, SkillsDictionary.Count);
            }

            // ��ѡ�еļ��ܷ��� skills ������
            skills[i] = SkillsDictionary[randomIndex];

            // ��ʹ�ù���������¼����
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
                // ���ü���
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
