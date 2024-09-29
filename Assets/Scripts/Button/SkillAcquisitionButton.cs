using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillAcquisitionButton : MonoBehaviour
{
    public Skill willGetSkill; // 要获取的技能

    public Button button; // 按钮组件

    public TextMeshProUGUI skillNameText; // 技能名称文本组件

    public Image skillIcon; // 技能图标组件


    void Start()
    {
        // 获取按钮组件
        button = GetComponent<Button>();

        // 检查按钮组件是否存在
        if (button != null)
        {
            // 添加点击事件监听器
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }

        // 更新按钮上的技能信息
        
    }

    public void ClearSkillInfo()
    {
        // 清空技能名称文本
        if (skillNameText != null)
        {
            skillNameText.text = "";
        }
        else
        {
            Debug.LogWarning("SkillNameText component not assigned.");
        }
    }

    // 按钮点击事件处理方法
    void OnButtonClick()
    {
        if (willGetSkill != null)
        {
            SkillAcquisitionManager.Instance.AcquireSkill(willGetSkill);
            Debug.Log($"Skill {willGetSkill.skillName} acquired.");
        }
        else
        {
            Debug.LogWarning("No skill assigned to this button.");
        }
    }

    // 更新按钮上的技能信息
    public void UpdateSkillInfo()
    {
        if (willGetSkill != null)
        {
            // 更新技能名称
            if (skillNameText != null)
            {
                skillNameText.text = willGetSkill.skillName;
            }
            else
            {
                Debug.LogWarning("SkillNameText component not assigned.");
            }

            // 更新技能图标
           
        }
        else
        {
            Debug.LogWarning("No skill assigned to this button.");
        }
    }

    //在玩家点击获取技能后将
}
