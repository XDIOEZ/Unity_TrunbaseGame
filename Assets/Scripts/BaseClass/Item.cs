using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int needActionPoints;


    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public int NeedActionPoints => needActionPoints;

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
    [ContextMenu("同步预制件文件名")]
    private void SyncPrefabFileName()
    {
        // 检查是否已设置预制件的名称和ID属性
        if (!string.IsNullOrEmpty(_name) && _id > 0) // 假设有一个id字段
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{_id}_{_name}";

            // 检查当前文件名是否与新文件名一致，避免不必要的更改
            if (this != null && !name.Equals(assetName))
            {
                // 重命名预制件的资产文件名
                string path = AssetDatabase.GetAssetPath(this);
                AssetDatabase.RenameAsset(path, assetName);
                AssetDatabase.SaveAssets();
            }
        }
        else
        {
            Debug.LogWarning("ID 或 预制件名称未设置。");
        }
    }
    #endif

}
