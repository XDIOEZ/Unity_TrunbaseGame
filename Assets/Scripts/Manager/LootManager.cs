using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    // ��������ģʽ
    public static LootManager Instance;
    [SerializeField]
    private GameObject ButtonPrefab;
    [SerializeField]
    private Transform buttonParentTransform;
    private void Awake()
    {

        // ȷ��ֻ����һ��ʵ��
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // ����ʵ���ڳ���֮��
        }
        else
        {
            Destroy(gameObject);
        }
        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 1;
    }
    // ��ȡ��ǰLevel�е�ս��Ʒ��
    public Canvas[] lootcanvases;


    public void SetLevelLoot(Level level)
    {
        // ȷ�� Level ��Ϊ null
        if (level == null)
        {
            Debug.LogError("Provided Level is null.");
            return;
        }

        // ��ȡս��Ʒ��
        Skill[] skillLoot = level.Level_Loot.skills;
        int maxClaimableTimes = level.Level_Loot.maxClaimableTimes; // ������ȡ����

        // ��ո�������Ӷ���ȷ�����ظ����ɰ�ť
        foreach (Transform child in buttonParentTransform)
        {
            Destroy(child.gameObject);
        }

        // ����ս��Ʒ������������ȡ�������ɰ�ť
        for (int i = 0; i < maxClaimableTimes; i++)
        {
            // ʵ������ťԤ����
            GameObject buttonPrefab = Instantiate(ButtonPrefab, buttonParentTransform);

            // ��ȡʵ����Ԥ�����LootButton���
            LootButton lootButton = buttonPrefab.GetComponent<LootButton>();

            // ���ѡ��ս��Ʒ�������䴫�����ť
            lootButton.SetToPlayerSkillList(RamdonGetloot(skillLoot));

            // ���°�ť���ı�������UI
            lootButton.UpdateLootText("Loot " + (i + 1));
        }
    }


    public Skill[] RamdonGetloot(Skill[] levelSkills_loot)
    {
        // ������������Ƿ�Ϊ��
        if (levelSkills_loot == null || levelSkills_loot.Length == 0)
        {
            Debug.LogError("The loot array is null or empty.");
            return new Skill[0]; // ����һ�������飬��ֹ��������ִ�б���
        }

        // ������鳤��С�ڵ���3������ȫ��Ԫ�أ��������ѡ��3��Ԫ��
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

        // ����ѡ���ս��Ʒ
        return selectedLoot;
    }

    public void OpenLootCanvas(int index)
    {
        //���ݴ����index��ͬ,�򿪲�ͬ��lootcanvases
        lootcanvases[index].enabled=true;
     
    }
}
