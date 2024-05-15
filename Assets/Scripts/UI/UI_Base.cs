using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _dict = new Dictionary<Type, UnityEngine.Object[]>();
    protected abstract void Init();

    private void Awake()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        _dict.Add(typeof(T), objs);

        for (int i = 0; i < names.Length; i++)
        {
            objs[i] = Util.FindChild<T>(gameObject, names[i]);

            if (objs[i] == null)
                Debug.Log($"바인드에 실패했습니다. : {names[i]}");
        }
    }    

    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindImage(Type type) => Bind<Image>(type);
    protected void BindTMP(Type type) => Bind<TextMeshProUGUI>(type);

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

    protected Button GetButton(int index) => Get<Button>(index);
    protected Image GetImage(int index) => Get<Image>(index);
    protected TextMeshProUGUI GetTMP(int index) => Get<TextMeshProUGUI>(index);
}
