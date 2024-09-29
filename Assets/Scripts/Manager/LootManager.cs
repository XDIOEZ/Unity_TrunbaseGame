using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    // 创建单例模式
    public static LootManager Instance;
    [SerializeField]
    private GameObject ButtonPrefab;
    [SerializeField]
    private Transform buttonParentTransform;
    private void Awake()
    {

        // 确保只创建一个实例
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 保持实例在场景之间
        }
        else
        {
            Destroy(gameObject);
        }
        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 1;
    }
    // 获取当前Level中的战利品表
    public Canvas[] lootcanvases;


    public void SetLevelLoot(Level level)
    {
        // 确保 Level 不为 null
        if (level == null)
        {
            Debug.LogError("Provided Level is null.");
            return;
        }

        // 获取战利品表
        Skill[] skillLoot = level.Level_Loot.skills;
        int maxClaimableTimes = level.Level_Loot.maxClaimableTimes; // 最大可领取次数

        // 清空父物体的子对象，确保不重复生成按钮
        foreach (Transform child in buttonParentTransform)
        {
            Destroy(child.gameObject);
        }

        // 遍历战利品表，根据最大可领取次数生成按钮
        for (int i = 0; i < maxClaimableTimes; i++)
        {
            // 实例化按钮预制体
            GameObject buttonPrefab = Instantiate(ButtonPrefab, buttonParentTransform);

            // 获取实例化预制体的LootButton组件
            LootButton lootButton = buttonPrefab.GetComponent<LootButton>();

            // 随机选择战利品，并将其传输给按钮
            lootButton.SetToPlayerSkillList(RamdonGetloot(skillLoot));

            // 更新按钮的文本或其他UI
            lootButton.UpdateLootText("Loot " + (i + 1));
        }
    }


    public Skill[] RamdonGetloot(Skill[] levelSkills_loot)
    {
        // 检查输入数组是否为空
        if (levelSkills_loot == null || levelSkills_loot.Length == 0)
        {
            Debug.LogError("The loot array is null or empty.");
            return new Skill[0]; // 返回一个空数组，防止后续代码执行报错
        }

        // 如果数组长度小于等于3，返回全部元素；否则随机选择3个元素
        Skill[] selectedLoot;
        if (levelSkills_loot.Length <= 3)
        {
            selectedLoot = levelSkills_loot;
            Debug.Log("Selected all available loot: " + string.Join(", ", selectedLoot.Select(s => s.name)));
        }
        else
        {
            selectedLoot = levelSkills_loot.OrderBy(x => Random.value).Take(3).ToArray();
            Debug.Log("Selected Loot: " + string.Join(", ", selectedLoot.Select(s => s.name)));
        }

        // 返回选择的战利品
        return selectedLoot;
    }

    public void OpenLootCanvas(int index)
    {
        //根据传入的index不同,打开不同的lootcanvases
        lootcanvases[index].enabled=true;
     
    }
}
