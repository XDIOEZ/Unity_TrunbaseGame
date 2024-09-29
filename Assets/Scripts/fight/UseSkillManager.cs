using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseSkillManager : MonoBehaviour
{
    //���õ���ģʽ
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
        // ȷ�� Level ��Ϊ null
        if (player == null)
        {
            Debug.LogError("Provided Level is null.");
            return;
        }

        // ��ȡս��Ʒ��
        List<Skill> skillList = player.Skills;

        // ��ո�������Ӷ���ȷ�����ظ����ɰ�ť
        foreach (Transform child in buttonParentTransform)
        {
            Destroy(child.gameObject);
        }

        // ����ս��Ʒ������������ȡ�������ɰ�ť
        for (int i = 0; i < skillList.Count; i++)
        {
            // ʵ������ťԤ����
            GameObject buttonPrefab = Instantiate(ButtonPrefab, buttonParentTransform);

            // ��ȡʵ����Ԥ�����LootButton���
            UseSkillButton ButtonButton = buttonPrefab.GetComponent<UseSkillButton>();

            // ���ѡ��ս��Ʒ�������䴫�����ť
            ButtonButton.SetSkill(skillList[i]);

            // ���°�ť���ı�������UI
            ButtonButton.UpdateText();
        }
    }
}
