using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableItem : InteractableFunctionality
{
    private string requiredKey;
    private InteractableFunctionality secondaryAction;
    private bool unlocked = false;

    public override void SetRequirement(string itemKey, InteractableFunctionality action)
    {
        requiredKey = itemKey;
        secondaryAction = action;
    }    

    public override void Action(bool isClicked, string key)
    {
        if (!unlocked && !isClicked && key != null && key == requiredKey) // unlock once something's dropped
        {
            Debug.Log("Unlocked!");
            unlocked = true;
            secondaryAction.Action(true); // assume the secondary action is click-only
        }
    }

    public override void ClearEffect()
    {
        if (unlocked)
        {
            secondaryAction.ClearEffect();
        }
    }
}
