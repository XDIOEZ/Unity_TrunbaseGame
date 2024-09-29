using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // 添加这行以便使用按钮组件

public class LootButton : MonoBehaviour
{
    // 需要同步的 TextMeshProUGUI 组件
    public TextMeshProUGUI lootText;

    // 需要同步的技能列表
    [SerializeField]
    private Skill[] toPlayerSkillList;

    public bool CanGetSkill = true; // 是否可以获取技能

    // 其他的将要获取的属性组

    // 初始化方法
    void Start()
    {
        // 确保 TextMeshProUGUI 组件已经被赋值
        if (lootText == null)
        {
            lootText = GetComponentInChildren<TextMeshProUGUI>();
        }

        // 获取按钮组件并绑定点击事件
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("No Button component found on this GameObject.");
        }
    }

    // 同步toPlayerSkillList方法
    public void SetToPlayerSkillList(Skill[] skillList)
       {
        toPlayerSkillList = skillList;
    }

    // 更新 TextMeshProUGUI 组件的文本
    public void UpdateLootText(string newText)
    {
        if (lootText != null)
        {
            lootText.text = newText;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    // 点击按钮时触发的事件
    public void OnButtonClick()
    {
        if (toPlayerSkillList != null && toPlayerSkillList.Length > 0)
        {   // 将技能列表传递给SkillAcquisitionManager

            SkillAcquisitionManager.Instance.SetSkillsForButton(this,toPlayerSkillList, CanGetSkill);
            SkillAcquisitionManager.Instance.canvas.enabled = true;
        }
        else
        {
            Debug.LogWarning("No skills assigned to this button.");
        }
    }
}
