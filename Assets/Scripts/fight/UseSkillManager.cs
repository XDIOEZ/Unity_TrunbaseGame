using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseSkillManager : MonoBehaviour
{
    //设置单例模式
    public static UseSkillManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] 
    private Player player;
    [SerializeField]
    private GameObject ButtonPrefab;
    [SerializeField]
    private Transform buttonParentTransform;
    public void InstantiationSkillButton(Player player)
    {
        // 确保 Level 不为 null
        if (player == null)
        {
            Debug.LogError("Provided Level is null.");
            return;
        }

        // 获取战利品表
        List<Skill> skillList = player.Skills;

        // 清空父物体的子对象，确保不重复生成按钮
        foreach (Transform child in buttonParentTransform)
        {
            Destroy(child.gameObject);
        }

        // 遍历战利品表，根据最大可领取次数生成按钮
        for (int i = 0; i < skillList.Count; i++)
        {
            // 实例化按钮预制体
            GameObject buttonPrefab = Instantiate(ButtonPrefab, buttonParentTransform);

            // 获取实例化预制体的LootButton组件
            UseSkillButton ButtonButton = buttonPrefab.GetComponent<UseSkillButton>();

            // 随机选择战利品，并将其传输给按钮
            ButtonButton.SetSkill(skillList[i]);

            // 更新按钮的文本或其他UI
            ButtonButton.UpdateText();
        }
    }
}
