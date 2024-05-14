using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    // To Do
    // 사용할 컴포넌트들을 캐싱해둘 자료구조
    // 컴포넌트 Bind
    // 컴포넌트 Get

    protected Dictionary<Type, UnityEngine.Object[]> _dict = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons
    {
        Popup,
        Linked,
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        _dict.Add(typeof(T), objs);

        for (int i = 0; i < names.Length; i++)
        {
            objs[i] = FindChild<T>(names[i]);

            if (objs[i] == null)
                Debug.Log($"바인드에 실패했습니다. : {names[i]}");
        }
    }

    private T FindChild<T>(string name) where T : UnityEngine.Object
    {
        foreach (T child in gameObject.GetComponentsInChildren<T>())
        {
            if (child.name == name)
                return child;
        }

        return null;
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs;

        if(_dict.TryGetValue(typeof(T), out objs) == false)
        {
            Debug.Log("컴포넌트를 가져오는데 실패했습니다.");
            return null;
        }

        return objs[index] as T;
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Button button = Get<Button>((int)Buttons.Linked);
    }
}
