using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "����һ����ʵ��")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name; // ʵ�������
    [SerializeField] private float _id; // ʵ������ʶ����
    [SerializeField] private float max_hp; // ���Ѫ��
    [SerializeField] private float hp; // ��ǰѪ��
    [SerializeField] private float max_mp; // �������
    [SerializeField] private float mp; // ��ǰ����
    [SerializeField] private float max_ap; // ����ж���
    [SerializeField] private float ap; // ��ǰ�ж���
    [SerializeField] private float max_dp; // ��������
    [SerializeField] private float dp; // ��ǰ������
    [SerializeField] private float max_mr; // ���������
    [SerializeField] private float mr; // ��ǰ������������
    [SerializeField] private Inventory entityInventory; // ʵ�������е���Ʒ
    public string Name => _name; // ֻ������
    public float ID => _id; // ֻ������
    public float MaxHp => max_hp; // ֻ������
    public float Hp => hp; // ֻ������
    public float MaxMp => max_mp; // ֻ������
    public float Mp => mp; // ֻ������
    public float MaxAp => max_ap; // ֻ������
    public float Ap => ap; // ֻ������
    public float MaxDp => max_dp; // ֻ������
    public float Dp => dp; // ֻ������
    public float MaxMr => max_mr; // ֻ������
    public float Mr => mr; // ֻ������
    public Inventory EntityInventory => entityInventory; // ֻ������

    #region ͬ���ļ���
#if UNITY_EDITOR
    [ContextMenu("ͬ���ļ���")]
    private void SyncFileName()
    {
        // ����Ƿ�������_levelName��_id����
        if (!string.IsNullOrEmpty(_name) && _id > 0)
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{_id}_{_name}";

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
            Debug.LogWarning("ID �� ����δ���á�");
        }
    }
#endif
    #endregion
}
