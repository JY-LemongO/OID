using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester : MonoBehaviour
{
    public Vector3 offset;

    private int index = 0;

    public void Popup()
    {
        if (index % 2 == 0)
            UIManager.Instance.ShowPopupUI<UI_TestPopup1>();
        else
            UIManager.Instance.ShowPopupUI<UI_TestPopup2>();
        index++;
    }

    public void Linked()
    {
        UIManager.Instance.ShowLinkedUI<UI_TestLinked1>();
    }
}
