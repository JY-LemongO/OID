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

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

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
    
    public GameObject GetFromPool(GameObject origin)
    {
        if (!_pools.ContainsKey(origin.name))
            CreatePool(origin);        

        return _pools[origin.name].Pop();
    }

    public void ReturnToPool(GameObject go)
    {
        _pools[go.name].Push(go);
    }

    public void CreatePool(GameObject origin)
    {
        Pool pool = new Pool();
        pool.origin = origin;
        pool.Init();

        _pools.Add(origin.name, pool);
    }
}
