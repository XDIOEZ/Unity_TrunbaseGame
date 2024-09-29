using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "自定义/Create New Character")]
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
    [ContextMenu("同步文件名")]
    private void SyncFileName()
    {
        // 检查是否已设置levelName和id属性
        if (!string.IsNullOrEmpty(characterName) && id > 0)
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{id}_{characterName}";

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
