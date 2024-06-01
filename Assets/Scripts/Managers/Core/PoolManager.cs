using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Queue<GameObject> poolQueue = new Queue<GameObject>();
    public GameObject origin;    

    private readonly int INIT_POOL_SIZE = 100;

    public void Init()
    {
        for (int i = 0; i < INIT_POOL_SIZE; i++)
            CreateObject();
    }

    public void CreateObject()
    {        
        GameObject clone = Object.Instantiate(origin);
        clone.name = origin.name;
        clone.SetActive(false);

        poolQueue.Enqueue(clone);
    }

    public GameObject Pop()
    {
        if (poolQueue.Count == 0)
            CreateObject();

        return poolQueue.Dequeue();
    }

    public void Push(GameObject go)
    {
        go.SetActive(false);
        poolQueue.Enqueue(go);        
    }
}

public class PoolManager
{
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
    
    public GameObject GetFromPool(string key, GameObject origin)
    {
        if (!_pools.ContainsKey(key))
            CreatePool(key, origin);        

        return _pools[origin.name].Pop();
    }

    public void ReturnToPool(GameObject go)
    {
        _pools[go.name].Push(go);
    }

    public void CreatePool(string key, GameObject origin)
    {
        Pool pool = new Pool();
        pool.origin = origin;
        pool.Init();

        _pools.Add(key, pool);
    }
}
