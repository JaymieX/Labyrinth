using UnityEngine;
using UnityEngine.SceneManagement;

public class Warpgate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if its a player that entered the warp gate
        if (other.tag == "Player")
        {
            var player = other.gameObject.GetComponent<PlayerController>();

            if (!player.CanWinGame) return; // Quit if player cannot win game yet

            // Setup winner data
            WinnerManager.WinnerId = player.id;
            WinnerManager.Gears = player.gears;

            // Load winning scene
            SceneManager.LoadScene("WinScene");
        }
    }
}
