using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AssetLoader
{
    // ���ڴ洢���ص��ʲ�����Ϊ�ʲ����ƣ�ֵΪ ScriptableObject
    private static Dictionary<string, ScriptableObject> loadedAssetsDict = new Dictionary<string, ScriptableObject>();

    public static void LoadAllScriptableObjects<T>(string groupName, System.Action<Dictionary<string, T>> onLoaded) where T : ScriptableObject
    {
        // ����Ѽ��أ�ֱ�ӷ���
        if (loadedAssetsDict.Count > 0)
        {
            onLoaded?.Invoke(ConvertToTypedDictionary<T>());
            return;
        }

        Addressables.LoadAssetsAsync<T>(groupName, null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var asset in handle.Result)
                {
                    loadedAssetsDict[asset.name] = asset; // ʹ���ʲ���������Ϊ��
                }

                onLoaded?.Invoke(ConvertToTypedDictionary<T>());
            }
            else
            {
                Debug.LogError("Failed to load assets from group: " + groupName);
                onLoaded?.Invoke(new Dictionary<string, T>()); // ���ؿ��ֵ���ָʾ����ʧ��
            }
        };
    }

    // �������������ֵ�ת��Ϊָ������
    private static Dictionary<string, T> ConvertToTypedDictionary<T>() where T : ScriptableObject
    {
        var result = new Dictionary<string, T>();
        foreach (var kvp in loadedAssetsDict)
        {
            if (kvp.Value is T typedValue)
            {
                result[kvp.Key] = typedValue;
            }
        }
        return result;
    }
}
