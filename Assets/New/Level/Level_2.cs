using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "�����µĹؿ�")]
public class Level_2 : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite level_sprite; // �ؿ�ͼ��
    [SerializeField] private EntityData enemy; // ͨ��ID�Ӵ����϶�ȡ��Ӧ��Enemy�����
    [SerializeField] private string describe; // �ؿ�����
    

    // ֻ������ss
    public int ID => _id;
    public string Name => _name;
    public Sprite LevelSprite => level_sprite; // ע���������������淶
    public EntityData Enemy => enemy;
    public string Describe => describe;

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
