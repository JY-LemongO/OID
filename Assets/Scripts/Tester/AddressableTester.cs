using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AddressableTester : MonoBehaviour
{
    [SerializeField] AssetReference aRef;

    GameObject go;

    private void Start()
    {
        aRef.InstantiateAsync().Completed +=
            (op) =>
        {
            go = op.Result;

            Invoke("NextScene", 5f);
        };
    }

    private void ReleaseGO()
    {
        aRef.ReleaseInstance(go);        
    }

    private void NextScene()
    {
        SceneManager.LoadScene("TestScene");
    }
}
