using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "创建一个新实体")]
public class EntityData : ScriptableObject
{
    [SerializeField] EntityBaseData entityBaseData; // 实体的基础数据

    public EntityBaseData EntityBaseData => entityBaseData; // 只读属性

    #region 同步文件名
#if UNITY_EDITOR
    [ContextMenu("同步文件名")]
    private void SyncFileName()
    {
        // 检查是否已设置_levelName和_id属性
        if (!string.IsNullOrEmpty(entityBaseData.name) && entityBaseData._id > 0)
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{entityBaseData._id}_{entityBaseData.name}";

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
            Debug.LogWarning("ID 或 名称未设置。");
        }
    }
#endif
    #endregion
}
