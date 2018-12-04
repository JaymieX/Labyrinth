using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneUiController : MonoBehaviour
{
    public Text WinningIdText;
    public Text WinningGearText;

    // Use this for initializations
    void Start()
    {
        WinningIdText.text = WinnerManager.WinnerId.ToString();
        WinningGearText.text = WinnerManager.Gears.ToString();
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
