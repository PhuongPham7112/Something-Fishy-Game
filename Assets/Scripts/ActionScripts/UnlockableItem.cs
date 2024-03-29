using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableItem : InteractableFunctionality
{
    [SerializeField] private bool destroyOnUnlock = false;
    [SerializeField] private float dialogueTime = 1.5f;
    private string requiredKey;
    private InteractableFunctionality secondaryAction = null;
    private bool unlocked = false;
    public List<string> unlockCue;
    private ConvoTrigger convo;

    private void Start()
    {
        convo = GetComponent<ConvoTrigger>();
    }

    public override void SetRequirement(string itemKey, InteractableFunctionality action)
    {
        requiredKey = itemKey;
        secondaryAction = action;
    }    

    public override void Action(bool isClicked, string key)
    {
        Debug.Log("key " + key + " vs required key " + requiredKey);
        if (!unlocked && !isClicked && key != null && key == requiredKey) // unlock once something's dropped
        {
            Debug.Log("Unlocked!");
            unlocked = true;
            if (convo != null)
            {
                Debug.Log("Playing cue");
                convo.currDialogue = new List<string>(unlockCue);
                convo.PlayDialogue(dialogueTime);
            }
            if (interactAudio != null)
            {
                interactAudio.Play();
            }
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
