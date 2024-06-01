using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TestLinked2 : UI_Linked
{
    public void Linked()
    {
        Managers.UI.ShowLinkedUI<UI_TestLinked3>();
    }

    public void Undo()
    {
        Managers.UI.UndoLinkedUI();
    }
}
