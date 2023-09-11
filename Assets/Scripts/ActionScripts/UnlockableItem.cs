using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableItem : InteractableFunctionality
{
    [SerializeField] private bool destroyOnUnlock = false;
    private string requiredKey;
    private InteractableFunctionality secondaryAction = null;
    private bool unlocked = false;

    public override void SetRequirement(string itemKey, InteractableFunctionality action)
    {
        requiredKey = itemKey;
        secondaryAction = action;
    }    

    public override void Action(bool isClicked, string key)
    {
        Debug.Log("key " + key + " vs required key " + requiredKey);
        Debug.Log(unlocked);
        Debug.Log(isClicked);
        Debug.Log(key);
        if (!unlocked && !isClicked && key != null && key == requiredKey) // unlock once something's dropped
        {
            Debug.Log("Unlocked!");
            unlocked = true;
            if (secondaryAction != null)
                secondaryAction.Action(true); // assume the secondary action is click-only
            if (destroyOnUnlock)
                Destroy(gameObject);
        }
    }

    public override void ClearEffect()
    {
        if (unlocked && secondaryAction != null)
        {
            secondaryAction.ClearEffect();
        }
    }
}
