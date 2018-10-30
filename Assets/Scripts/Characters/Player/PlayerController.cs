using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // States
    private IPlayerState _playerWeaponTypeState;

    internal IPlayerState PlayerWeaponTypeState
    {
        get { return _playerWeaponTypeState; }
        set { _playerWeaponTypeState.End(); _playerWeaponTypeState = value; _playerWeaponTypeState.Begin(); }
    }

    // Use this for initialization
    void Start()
    {
        _playerWeaponTypeState = new WeaponIdleState();
        _playerWeaponTypeState.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        // Master controls

        // Open Gate
        if (Input.GetKeyDown(KeyCode.E))
        {
            var openAction = PlayerManager.Instance.OnPlayerOpenInteract;
            if (openAction != null)
            {
                openAction();
            }
        }

        // Handle any state input
        PlayerWeaponTypeState.HandleInput();

        // Change state if needed
        IPlayerState nextState = PlayerWeaponTypeState.UpdateState();
        if (nextState != null)
        {
            PlayerWeaponTypeState = nextState;
        }

        PlayerManager.Instance.PlayerPos = transform.position;
    }

    public bool CastRay(float range, out RaycastHit info)
    {
        // Ray cast
        Vector3 origin = transform.position;
        origin.y += 0.6f;

        Debug.DrawRay(origin, transform.forward * range, Color.red);
        return Physics.Raycast(origin, transform.forward, out info, range);
    }
}
