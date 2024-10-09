using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AssetLoader
{
    // 用于存储加载的资产，键为资产名称，值为 ScriptableObject
    private static Dictionary<string, ScriptableObject> loadedAssetsDict = new Dictionary<string, ScriptableObject>();

    public static void LoadAllScriptableObjects<T>(string groupName, System.Action<Dictionary<string, T>> onLoaded) where T : ScriptableObject
    {
        // 如果已加载，直接返回
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
                    loadedAssetsDict[asset.name] = asset; // 使用资产的名字作为键
                }

                onLoaded?.Invoke(ConvertToTypedDictionary<T>());
            }
            else
            {
                Debug.LogError("Failed to load assets from group: " + groupName);
                onLoaded?.Invoke(new Dictionary<string, T>()); // 返回空字典以指示加载失败
            }
        };
    }

    // 辅助方法：将字典转换为指定类型
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
