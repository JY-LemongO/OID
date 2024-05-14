using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TestLinked1 : UI_Linked
{
    public void Linked()
    {
        UIManager.Instance.ShowLinkedUI<UI_TestLinked2>();
    }

    public void Undo()
    {
        UIManager.Instance.UndoLinkedUI();
    }
}
