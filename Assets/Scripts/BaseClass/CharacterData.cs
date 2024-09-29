using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "�Զ���/Create New Character")]
public class CharacterData : ScriptableObject
{
    public int id;
    public string characterName;
    public int level;
    public float maxHp;
    public float attack;
    public float defend;
    public float maxAction;
    public int SkillsCount;
    public List<Skill> baseSkills;
#if UNITY_EDITOR
    [ContextMenu("ͬ���ļ���")]
    private void SyncFileName()
    {
        // ����Ƿ�������levelName��id����
        if (!string.IsNullOrEmpty(characterName) && id > 0)
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{id}_{characterName}";

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
            Debug.LogWarning("ID �� �ؿ�����δ���á�");
        }
    }
#endif
}
