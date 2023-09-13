using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFunctionality : MonoBehaviour
{
    public AudioSource interactAudio;
    public virtual void Action(bool isClicked, string key = "") { return;  }

    public virtual void ClearEffect() { return; }

    public virtual void SetRequirement(string requiredKey, InteractableFunctionality requiredSecondAction) { }
}

