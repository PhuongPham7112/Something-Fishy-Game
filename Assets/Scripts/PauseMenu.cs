using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button quitButton;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            pauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            pauseMenuUI.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        // Add code here to restart your game
    }

    public void QuitGame()
    {
        // Add code here to quit your game
        Application.Quit();
        // If you're in the Unity Editor, you might want to stop play mode
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
