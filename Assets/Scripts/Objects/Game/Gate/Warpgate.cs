using UnityEngine;
using UnityEngine.SceneManagement;

public class Warpgate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if its a player that entered the warp gate
        if (other.tag == "Player")
        {
            // Setup winner data
            var player = other.gameObject.GetComponent<PlayerController>();
            WinnerManager.WinnerId = player.id;
            WinnerManager.Gears = player.gears;

            // Load winning scene
            SceneManager.LoadScene("WinScene");
        }
    }
}
