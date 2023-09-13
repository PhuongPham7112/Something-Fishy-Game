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
            Debug.Log("Escapable");
            isEscapable = true;
            Time.timeScale = 0f; // stop everything
            SceneManager.LoadScene("EndingScene");
        }
    }
}
