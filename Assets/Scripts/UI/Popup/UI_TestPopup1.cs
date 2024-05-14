using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TestPopup1 : UI_Popup
{
    public void Close()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
}
