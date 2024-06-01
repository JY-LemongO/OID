using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject testPrefab;

    [Header("Settings")]
    public float spawnDelay;    
    public float spawnOffset;    

    private WaitForSeconds spawnTime;

    private void Start()
    {
        //Managers.RM.LoadAllAsync<GameObject>("Test", (key, count, totalCount) =>
        //{
        //    Debug.Log($"{key} {count}/{totalCount}");
        //});

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
        //GameObject clone = Managers.Pool.GetFromPool(testPrefab);
        //clone.SetActive(true);

        //clone.transform.position = Random.insideUnitSphere * spawnOffset;
    }
}
