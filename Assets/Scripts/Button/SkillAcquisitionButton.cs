using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillAcquisitionButton : MonoBehaviour
{
    public Skill willGetSkill; // Ҫ��ȡ�ļ���

    public Button button; // ��ť���

    public TextMeshProUGUI skillNameText; // ���������ı����

    public Image skillIcon; // ����ͼ�����


    void Start()
    {
        // ��ȡ��ť���
        button = GetComponent<Button>();

        // ��鰴ť����Ƿ����
        if (button != null)
        {
            // ��ӵ���¼�������
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }

        // ���°�ť�ϵļ�����Ϣ
        
    }

    public void ClearSkillInfo()
    {
        // ��ռ��������ı�
        if (skillNameText != null)
        {
            skillNameText.text = "";
        }
        else
        {
            Debug.LogWarning("SkillNameText component not assigned.");
        }
    }

    // ��ť����¼�������
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

    // ���°�ť�ϵļ�����Ϣ
    public void UpdateSkillInfo()
    {
        if (willGetSkill != null)
        {
            // ���¼�������
            if (skillNameText != null)
            {
                skillNameText.text = willGetSkill.skillName;
            }
            else
            {
                Debug.LogWarning("SkillNameText component not assigned.");
            }

            // ���¼���ͼ��
           
        }
        else
        {
            Debug.LogWarning("No skill assigned to this button.");
        }
    }

    //����ҵ����ȡ���ܺ�
}
