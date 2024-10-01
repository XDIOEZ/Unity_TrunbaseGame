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
    [ContextMenu("ͬ��Ԥ�Ƽ��ļ���")]
    private void SyncPrefabFileName()
    {
        // ����Ƿ�������Ԥ�Ƽ������ƺ�ID����
        if (!string.IsNullOrEmpty(_name) && _id > 0) // ������һ��id�ֶ�
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{_id}_{_name}";

            // ��鵱ǰ�ļ����Ƿ������ļ���һ�£����ⲻ��Ҫ�ĸ���
            if (this != null && !name.Equals(assetName))
            {
                // ������Ԥ�Ƽ����ʲ��ļ���
                string path = AssetDatabase.GetAssetPath(this);
                AssetDatabase.RenameAsset(path, assetName);
                AssetDatabase.SaveAssets();
            }
        }
        else
        {
            Debug.LogWarning("ID �� Ԥ�Ƽ�����δ���á�");
        }
    }
    #endif

}
