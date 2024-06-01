using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TestLinked1 : UI_Linked
{
    public void Linked()
    {
        Managers.UI.ShowLinkedUI<UI_TestLinked2>();
    }

    public void Undo()
    {
        Managers.UI.UndoLinkedUI();
    }
}
