using UnityEditor;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;
    public string description;
    public float needActionPoints;//消耗行动点数
        
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
    [ContextMenu("同步文件名")]
    private void SyncFileName()
    {
        // 检查是否已设置levelName和id属性
        if (!string.IsNullOrEmpty(skillName) && skillID > 0)
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{skillID}_{skillName}";

            // 检查当前文件名是否与新文件名一致，避免不必要的更改
            if (this != null && !name.Equals(assetName))
            {
                // 重命名 ScriptableObject 的资产文件名
                string path = AssetDatabase.GetAssetPath(this);
                AssetDatabase.RenameAsset(path, assetName);
                AssetDatabase.SaveAssets();
            }
        }
        else
        {
            Debug.LogWarning("ID 或 关卡名称未设置。");
        }
    }
#endif
}
