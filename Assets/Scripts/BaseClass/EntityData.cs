using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "创建一个新实体")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name; // 实体的名字
    [SerializeField] private float _id; // 实体的身份识别码
    [SerializeField] private float max_hp; // 最大血量
    [SerializeField] private float hp; // 当前血量
    [SerializeField] private float max_mp; // 最大蓝量
    [SerializeField] private float mp; // 当前蓝量
    [SerializeField] private float max_ap; // 最大行动点
    [SerializeField] private float ap; // 当前行动点
    [SerializeField] private float max_dp; // 最大防御力
    [SerializeField] private float dp; // 当前防御力
    [SerializeField] private float max_mr; // 最大法术抗性
    [SerializeField] private float mr; // 当前法术抗性数据
    [SerializeField] private Inventory entityInventory; // 实体所持有的物品
    public string Name => _name; // 只读属性
    public float ID => _id; // 只读属性
    public float MaxHp => max_hp; // 只读属性
    public float Hp => hp; // 只读属性
    public float MaxMp => max_mp; // 只读属性
    public float Mp => mp; // 只读属性
    public float MaxAp => max_ap; // 只读属性
    public float Ap => ap; // 只读属性
    public float MaxDp => max_dp; // 只读属性
    public float Dp => dp; // 只读属性
    public float MaxMr => max_mr; // 只读属性
    public float Mr => mr; // 只读属性
    public Inventory EntityInventory => entityInventory; // 只读属性

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
