using UnityEditor;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;
    public string description;
    public float needActionPoints;//�����ж�����
        
    public virtual void Use(Character user, Character target)
    {
       
    }

    public virtual bool ChangeActionPoints(Character user)
    {
        if (user.CurrentAction - needActionPoints < 0)
        {
            Debug.Log("Not enough action points");
            return false;
        }
        user.CurrentAction -= needActionPoints;
        return true;
    }
#if UNITY_EDITOR
    [ContextMenu("ͬ���ļ���")]
    private void SyncFileName()
    {
        // ����Ƿ�������levelName��id����
        if (!string.IsNullOrEmpty(skillName) && skillID > 0)
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{skillID}_{skillName}";

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
