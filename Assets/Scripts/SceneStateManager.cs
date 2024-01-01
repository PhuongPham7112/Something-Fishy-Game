using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayGameScene()
    {
        GameState.isPlaying = true;
        SceneManager.LoadScene("GameScene");
    }
    public void PlayStartScene()
    {
        GameState.isPlaying = true;
        SceneManager.LoadScene("StartingScene");
    }

    public void PlayEndScene()
    {
        GameState.isPlaying = false;
        SceneManager.LoadScene("EndingScene");
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
 