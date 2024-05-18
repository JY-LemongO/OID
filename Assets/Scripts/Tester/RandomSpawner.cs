using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject testPrefab;

    [Header("Settings")]
    public float spawnDelay;    
    public float spawnOffset;
    public int countPerSpawn;

    private WaitForSeconds spawnTime;

    private void Start()
    {
        spawnTime = new WaitForSeconds(spawnDelay);
        StartCoroutine(Co_SpawnEndless());
    }    

    private IEnumerator Co_SpawnEndless()
    {
        while (true)
        {
            SpawnClone();

            yield return spawnTime;
        }        
    }

    private void SpawnClone()
    {
        for (int i = 0; i < countPerSpawn; i++)
        {
            // 생성
            //GameObject clone = Instantiate(testPrefab);

            // 풀링
            GameObject clone = PoolManager.Instance.GetFromPool(testPrefab);
            clone.SetActive(true);

            clone.transform.position = Random.insideUnitSphere * spawnOffset;
        }
    }
}
