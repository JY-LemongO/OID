using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out Object obj) == true)
            return obj as T;

        return null;
    }

    public GameObject Instantiate(string key, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>(key);

        if (pooling)
            return Managers.Pool.GetFromPool(key, prefab);

        GameObject go = Object.Instantiate(prefab);
        go.name = prefab.name;

        return go;
    }

    public void Destroy(GameObject go)
    {

    }

    #region 어드레서블
    public void LoadAsync<T>(string key, Action<T> callBack = null) where T : UnityEngine.Object
    {
        if(_resources.TryGetValue(key, out Object resource))
        {
            callBack?.Invoke(resource as T);
            return;
        }

        var asyncOp = Addressables.LoadAssetAsync<T>(key);
        asyncOp.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            callBack?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callBack = null) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int count = 0;
            int totalCount = op.Result.Count;

            foreach(var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    count++;
                    callBack?.Invoke(result.PrimaryKey, count, totalCount);
                });
            }
        };
    }
    #endregion
}
