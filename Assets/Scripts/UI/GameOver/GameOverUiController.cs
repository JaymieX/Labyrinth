using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUiController : MonoBehaviour
{
    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("Quitting game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
