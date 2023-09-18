using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeItem : InteractableFunctionality
{
    private bool isEscapable = false;
    public override void Action(bool isClicked, string key = "")
    {
        if (!isEscapable) // this is a click only object
        {
            if (interactAudio != null)
            {
                interactAudio.Play();
            }
            isEscapable = true;
            GameState.isEscape = isEscapable;
            Time.timeScale = 0f; // stop everything
            SceneStateManager.Instance.PlayEndScene();
        }
    }
}
