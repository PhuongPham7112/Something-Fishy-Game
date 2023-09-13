using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockItem : InteractableFunctionality
{
    // Start is called before the first frame update
    private bool isEscapable = false;
    public override void Action(bool isClicked, string key = "")
    {
        if (isClicked && !isEscapable) // this is a click only object
        {
            isEscapable = true;
            if (interactAudio != null)
            {
                interactAudio.Play();
            }
        }
    }
}
