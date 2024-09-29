using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewLevel", menuName = "自定义/Create New Level")]
public class Level : ScriptableObject
{
    public int id; // Level ID
    public Sprite Level_sprite; // 关卡图标
    public string level_Name; // 关卡名称
    public string level_Description; // 关卡描述

    public Loot Level_Loot;//战利品表
    public CharacterData[] Level_Battlecharacter; // 角色数值
    public int[] Level_useSkillList; // 使用技能ID数组
#if UNITY_EDITOR
    // 添加右键菜单项
    [ContextMenu("同步文件名")]
    private void SyncFileName()
    {
        // 检查是否已设置levelName和id属性
        if (!string.IsNullOrEmpty(level_Name) && id > 0)
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{id}_{level_Name}";

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
