using UnityEngine;

public class LGameManager : MonoBehaviour
{
    public PlayerController PlayerController;

    // Use this for initialization
    void Start()
    {
        PlayerManager.Instance.ResetAll();

        PlayerManager.Instance.PlayerController = PlayerController;
    }
}
