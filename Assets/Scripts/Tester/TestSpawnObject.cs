using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnObject : MonoBehaviour
{
    private bool _isInit = false;

    private WaitForSeconds wait = new WaitForSeconds(1);

    private void OnEnable()
    {
        if (!_isInit)
        {
            _isInit = true;
            return;
        }

        StartCoroutine(Co_DestroyRoutine());
    }

    private IEnumerator Co_DestroyRoutine()
    {
        yield return wait;

        //파괴
        //Destroy(gameObject);

        //풀링
        Managers.Pool.ReturnToPool(gameObject);
    }
}
