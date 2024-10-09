using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "����һ����ʵ��")]
public class EntityData : ScriptableObject
{
    [SerializeField] EntityBaseData entityBaseData; // ʵ��Ļ�������

    public EntityBaseData EntityBaseData => entityBaseData; // ֻ������

    #region ͬ���ļ���
#if UNITY_EDITOR
    [ContextMenu("ͬ���ļ���")]
    private void SyncFileName()
    {
        // ����Ƿ�������_levelName��_id����
        if (!string.IsNullOrEmpty(entityBaseData.name) && entityBaseData._id > 0)
        {
            // ���ļ�������Ϊ��ID_���ơ�
            string assetName = $"{entityBaseData._id}_{entityBaseData.name}";

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
