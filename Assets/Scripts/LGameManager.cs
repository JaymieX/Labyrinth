using UnityEngine;

public class LGameManager : MonoBehaviour
{
    public PlayerController PlayerController1;
    public PlayerController PlayerController2;
    public GameObject PauseUI;

    public RangeWeaponInfo[] RangeWeapons;

    private bool _pause;

    // Use this for initialization
    void Start()
    {
        _pause = false; // Don't pause at start


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
}
