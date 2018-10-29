using UnityEngine;

public class LGameManager : MonoBehaviour
{
    public CharacterController CharacterController;
    public PlayerController PlayerController;

    // Use this for initialization
    void Start()
    {
        PlayerManager.Instance.ResetAll();

        PlayerManager.Instance.PlayerCharacterController = CharacterController;
        PlayerManager.Instance.PlayerController = PlayerController;
    }
}
