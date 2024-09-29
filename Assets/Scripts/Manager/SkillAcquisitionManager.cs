using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAcquisitionManager : MonoBehaviour
{
    // 单例实例
    public static SkillAcquisitionManager Instance;
    public Canvas canvas;
    // 玩家技能列表
    public PlayerSkillInventory playerSkills;
    // 获取技能的按钮
    public SkillAcquisitionButton[] skillButton;
    [SerializeField]
    LootButton _lootButton;

    private void Awake()
    {
        // 实现单例模式
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 如果需要在场景之间保留单例实例
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetSkillsForButton(LootButton lootButton, Skill[] levelSkills_loot, bool CanGetSkill)// 遍历LootManager中的技能战利品，并将其添加到levelSkills列表中
    {
        _lootButton = lootButton;
        int minCount = Mathf.Min(skillButton.Length, levelSkills_loot.Length);

        for (int i = 0; i < minCount; i++)
        {
            skillButton[i].willGetSkill = levelSkills_loot[i];
            // 更新按钮的技能信息
            skillButton[i].UpdateSkillInfo();
            skillButton[i].button.interactable = CanGetSkill; // 根据 CanGetSkill 设置按钮状态
        }

        // 如果 skillButton 数组的大小大于 levelSkills_loot 数组的大小
        // 你可以在这里禁用多余的按钮，或者采取其他适当的处理措施
        for (int i = minCount; i < skillButton.Length; i++)
        {
            skillButton[i].button.interactable = false; // 禁用多余的按钮
            skillButton[i].ClearSkillInfo(); // 清除技能信息
        }
    }
    public void AcquireSkill(Skill skill)// 添加特定技能到玩家的技能列表
    {
        if (skill == null)
        {
            Debug.LogWarning("Attempted to acquire a null skill.");
            return;
        }

        // 使用 PlayerSkillInventory 的方法管理技能
        if (playerSkills != null)
        {
            bool success = playerSkills.AddSkill(skill);
            if (success)
            {
                Debug.Log($"Skill {skill.skillName} acquired and added to player's skills.");

                // 禁用所有技能获取按钮
                _lootButton.CanGetSkill = false; // 更新 CanGetSkill 属性
                UpdateButtonStates(_lootButton.CanGetSkill); // 更新所有按钮的状态
            }
            else
            {
                Debug.LogWarning($"Skill {skill.skillName} is already acquired.");
            }
        }
        else
        {
            Debug.LogError("PlayerSkills inventory is not assigned.");
        }
    }
    private void UpdateButtonStates(bool CanGetSkill)// 更新所有按钮的状态至传入的 CanGetSkill 值
    {
        foreach (SkillAcquisitionButton button in skillButton)
        {
            button.button.interactable = CanGetSkill; // 根据 CanGetSkill 更新按钮状态
        }
    }
}
