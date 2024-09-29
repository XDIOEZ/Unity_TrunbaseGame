using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLoot", menuName = "�Զ���/Create New Loot")]

public class Loot : ScriptableObject
{
    public int id;
    public string lootName;

    // ��SkillLootԪ����Ϊ�����������ֶ�
    public Skill[] skills;  // ��������
    public int maxClaimableTimes; // ������ȡ����
                                  //ֻ�ڱ༭������,������ʱ����bug
#if UNITY_EDITOR
    [ContextMenu("ͬ���ļ���")]
    private void SyncFileName()
    {
        // ����Ƿ������� lootName �� Id ����
        if (!string.IsNullOrEmpty(lootName) && id > 0)
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{id}_{lootName}";

            // ��鵱ǰ�ļ����Ƿ������ļ���һ�£����ⲻ��Ҫ�ĸ���
            if (this != null && !name.Equals(assetName))
            {
                // ������ ScriptableObject ���ʲ��ļ���
                string path = AssetDatabase.GetAssetPath(this);
                AssetDatabase.RenameAsset(path, assetName);
                AssetDatabase.SaveAssets();
            }
        }
        else
        {
            Debug.LogWarning("ID �� ս��Ʒ����δ���á�");
        }
    }
#endif
}
