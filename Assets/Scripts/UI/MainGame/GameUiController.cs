using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    public Text GearCount;
    public Image HpBar;

    public GameObject DeathScreen;

    public PlayerController player;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Update health bar
        HpBar.transform.localScale =
            new Vector3(player.health / 100.0f, 1f, 1f);

        GearCount.text = player.gears.ToString();
    }

    // Show death screen
    public void OnDeath()
    {
        DeathScreen.SetActive(true);
    }

    // Show alive screen
    public void OnAlive()
    {
        DeathScreen.SetActive(false);
    }
}
