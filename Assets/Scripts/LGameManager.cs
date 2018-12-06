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

        // Change view port if game is single player
        if (GameInit.IsSinglePlayer)
        {
            PlayerController2.gameObject.SetActive(false);
            var cam = PlayerController1.gameObject.transform.GetChild(0).GetComponent<Camera>();
            cam.rect = new Rect(0f, 0f, 1f, 1f);
        }
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
