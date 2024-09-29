using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // ��������Ա�ʹ�ð�ť���

public class LootButton : MonoBehaviour
{
    // ��Ҫͬ���� TextMeshProUGUI ���
    public TextMeshProUGUI lootText;

    // ��Ҫͬ���ļ����б�
    [SerializeField]
    private Skill[] toPlayerSkillList;

    public bool CanGetSkill = true; // �Ƿ���Ի�ȡ����

    // �����Ľ�Ҫ��ȡ��������

    // ��ʼ������
    void Start()
    {
        // ȷ�� TextMeshProUGUI ����Ѿ�����ֵ
        if (lootText == null)
        {
            lootText = GetComponentInChildren<TextMeshProUGUI>();
        }

        // ��ȡ��ť������󶨵���¼�
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

    // ͬ��toPlayerSkillList����
    public void SetToPlayerSkillList(Skill[] skillList)
       {
        toPlayerSkillList = skillList;
    }

    // ���� TextMeshProUGUI ������ı�
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

    // �����ťʱ�������¼�
    public void OnButtonClick()
    {
        if (toPlayerSkillList != null && toPlayerSkillList.Length > 0)
        {   // �������б��ݸ�SkillAcquisitionManager

            SkillAcquisitionManager.Instance.SetSkillsForButton(this,toPlayerSkillList, CanGetSkill);
            SkillAcquisitionManager.Instance.canvas.enabled = true;
        }
        else
        {
            Debug.LogWarning("No skills assigned to this button.");
        }
    }
}
