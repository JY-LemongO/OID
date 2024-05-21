using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class ResourceManager : MonoBehaviour
{
    public ResourceManager Instance;

    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadAsync<T>(string key, Action<T> callBack = null) where T : UnityEngine.Object
    {
        if(_resources.TryGetValue(key, out Object resource))
        {
            callBack?.Invoke(resource as T);
            return;
        }

        AsyncOperationHandle<T> asyncOp = Addressables.LoadAssetAsync<T>(key);
        asyncOp.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            callBack?.Invoke(op.Result);
        };
    }
}
