using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : InteractableFunctionality
{
    // Start is called before the first frame update
    public override void Action(bool isClicked, string key = "")
    {
        Debug.Log("Reading this");
    }

    public override void ClearEffect()
    {
        Debug.Log("Stop reading");
    }
}
