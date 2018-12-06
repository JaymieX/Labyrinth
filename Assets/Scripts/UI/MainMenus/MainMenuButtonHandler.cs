using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public void OnButtonPlayClick(bool single)
    {
        GameInit.IsSinglePlayer = single;
        SceneManager.LoadScene("GameScene");
    }

    public void OnButtonQuitClick()
    {
        Debug.Log("Quitting game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}