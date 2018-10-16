using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public void OnButtonPlayClick()
    {
        SceneManager.LoadScene("CharacterCreateScene");
    }
}