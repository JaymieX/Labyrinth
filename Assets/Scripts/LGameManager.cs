using UnityEngine;

public class LGameManager : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject PauseUI;

    public RangeWeaponInfo[] RangeWeapons;

    private bool _pause;

    // Use this for initialization
    void Start()
    {
        _pause = false;
        PauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause feature
            if (_pause) // Unpause
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                PauseUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else // Pause
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                PauseUI.SetActive(true);
                Time.timeScale = 0f;
            }

            _pause = !_pause;
        }
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
