using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLoot", menuName = "自定义/Create New Loot")]

public class Loot : ScriptableObject
{
    public int id;
    public string lootName;

    // 将SkillLoot元组拆分为两个单独的字段
    public Skill[] skills;  // 技能数组
    public int maxClaimableTimes; // 最大可领取次数
                                  //只在编辑器可用,避免打包时产生bug
#if UNITY_EDITOR
    [ContextMenu("同步文件名")]
    private void SyncFileName()
    {
        // 检查是否已设置 lootName 和 Id 属性
        if (!string.IsNullOrEmpty(lootName) && id > 0)
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{id}_{lootName}";

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
            Debug.LogWarning("ID 或 战利品名称未设置。");
        }
    }
#endif
}
