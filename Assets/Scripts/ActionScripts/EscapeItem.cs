using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeItem : InteractableFunctionality
{
    private bool isEscapable = false;
    public override void Action(bool isClicked, string key = "")
    {
        if (isClicked && !isEscapable) // this is a click only object
        {
            Debug.Log("Escapable");
            isEscapable = true;
        }
    }
}
