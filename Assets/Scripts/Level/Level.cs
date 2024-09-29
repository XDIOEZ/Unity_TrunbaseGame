using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewLevel", menuName = "�Զ���/Create New Level")]
public class Level : ScriptableObject
{
    public int id; // Level ID
    public Sprite Level_sprite; // �ؿ�ͼ��
    public string level_Name; // �ؿ�����
    public string level_Description; // �ؿ�����

    public Loot Level_Loot;//ս��Ʒ��
    public CharacterData[] Level_Battlecharacter; // ��ɫ��ֵ
    public int[] Level_useSkillList; // ʹ�ü���ID����
#if UNITY_EDITOR
    // ����Ҽ��˵���
    [ContextMenu("ͬ���ļ���")]
    private void SyncFileName()
    {
        // ����Ƿ�������levelName��id����
        if (!string.IsNullOrEmpty(level_Name) && id > 0)
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{id}_{level_Name}";

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
