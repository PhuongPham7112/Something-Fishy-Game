using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenItem : InteractableFunctionality
{
    private void Awake()
    {
        gameObject.SetActive(false);    
    }
    
    public override void Action(bool isClicked, string key = "")
    {
        gameObject.SetActive(true);
    }
}
