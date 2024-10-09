using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "创建新的关卡")]
public class Level_2 : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite level_sprite; // 关卡图标
    [SerializeField] private EntityData enemy; // 通过ID从磁盘上读取对应的Enemy类输出
    [SerializeField] private string describe; // 关卡描述
    

    // 只读属性ss
    public int ID => _id;
    public string Name => _name;
    public Sprite LevelSprite => level_sprite; // 注意属性名的命名规范
    public EntityData Enemy => enemy;
    public string Describe => describe;

    #region 同步文件名
#if UNITY_EDITOR
    [ContextMenu("同步文件名")]
    private void SyncFileName()
    {
        // 检查是否已设置_levelName和_id属性
        if (!string.IsNullOrEmpty(_name) && _id > 0)
        {
            // 将文件名设置为“ID_名称”
            string assetName = $"{_id}_{_name}";

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
