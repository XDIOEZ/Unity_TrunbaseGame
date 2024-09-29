using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAcquisitionManager : MonoBehaviour
{
    // ����ʵ��
    public static SkillAcquisitionManager Instance;
    public Canvas canvas;
    // ��Ҽ����б�
    public PlayerSkillInventory playerSkills;
    // ��ȡ���ܵİ�ť
    public SkillAcquisitionButton[] skillButton;
    [SerializeField]
    LootButton _lootButton;

    private void Awake()
    {
        // ʵ�ֵ���ģʽ
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // �����Ҫ�ڳ���֮�䱣������ʵ��
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetSkillsForButton(LootButton lootButton, Skill[] levelSkills_loot, bool CanGetSkill)// ����LootManager�еļ���ս��Ʒ����������ӵ�levelSkills�б���
    {
        _lootButton = lootButton;
        int minCount = Mathf.Min(skillButton.Length, levelSkills_loot.Length);

        for (int i = 0; i < minCount; i++)
        {
            skillButton[i].willGetSkill = levelSkills_loot[i];
            // ���°�ť�ļ�����Ϣ
            skillButton[i].UpdateSkillInfo();
            skillButton[i].button.interactable = CanGetSkill; // ���� CanGetSkill ���ð�ť״̬
        }

        // ��� skillButton ����Ĵ�С���� levelSkills_loot ����Ĵ�С
        // �������������ö���İ�ť�����߲�ȡ�����ʵ��Ĵ����ʩ
        for (int i = minCount; i < skillButton.Length; i++)
        {
            skillButton[i].button.interactable = false; // ���ö���İ�ť
            skillButton[i].ClearSkillInfo(); // ���������Ϣ
        }
    }
    public void AcquireSkill(Skill skill)// ����ض����ܵ���ҵļ����б�
    {
        if (skill == null)
        {
            Debug.LogWarning("Attempted to acquire a null skill.");
            return;
        }

        // ʹ�� PlayerSkillInventory �ķ���������
        if (playerSkills != null)
        {
            bool success = playerSkills.AddSkill(skill);
            if (success)
            {
                Debug.Log($"Skill {skill.skillName} acquired and added to player's skills.");

                // �������м��ܻ�ȡ��ť
                _lootButton.CanGetSkill = false; // ���� CanGetSkill ����
                UpdateButtonStates(_lootButton.CanGetSkill); // �������а�ť��״̬
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
    private void UpdateButtonStates(bool CanGetSkill)// �������а�ť��״̬������� CanGetSkill ֵ
    {
        foreach (SkillAcquisitionButton button in skillButton)
        {
            button.button.interactable = CanGetSkill; // ���� CanGetSkill ���°�ť״̬
        }
    }
}
