using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private List<UI_Linked> _linkList = new List<UI_Linked>();
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    private int _order = 5;

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

    public T ShowPopupUI<T>(string path = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(path))
            path = typeof(T).Name;

        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Popup/{path}");

        if(prefab == null)
        {
            Debug.Log("경로가 올바르지 않습니다.");
            return null;
        }

        T popup = Instantiate(prefab).GetOrAddComponent<T>();
        _popupStack.Push(popup);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.LogWarning("팝업이 일치하지 않습니다.");
            return;
        }

        UI_Popup stackPopup = _popupStack.Pop();
        Destroy(stackPopup.gameObject);
        _order--;
    }    

    public T ShowLinkedUI<T>(string path = null) where T : UI_Linked
    {
        if(string.IsNullOrEmpty(path))
            path = typeof(T).Name;

        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Linked/{path}");

        if (prefab == null)
        {
            Debug.Log("경로가 올바르지 않습니다.");
            return null;
        }

        if (_linkList.Count > 0)
            _linkList[_linkList.Count - 1].gameObject.SetActive(false);

        T linked = Instantiate(prefab).GetOrAddComponent<T>();
        _linkList.Add(linked);

        return linked;
    }

    public void UndoLinkedUI()
    {
        if (_linkList.Count == 0)
            return;        

        UI_Linked lastUI = _linkList[_linkList.Count - 1];
        _linkList.RemoveAt(_linkList.Count - 1);
        Destroy(lastUI.gameObject);

        if (_linkList.Count > 0)
            _linkList[_linkList.Count - 1].gameObject.SetActive(true);
    }
}
