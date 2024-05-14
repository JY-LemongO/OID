using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TestLinked3 : UI_Linked
{
    public void Undo()
    {
        UIManager.Instance.UndoLinkedUI();
    }
}
