using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal Vector3 MoveDirection = Vector3.zero;

    private CharacterController _characterController;

    // States
    private IPlayerState _playerState;
    private IPlayerState _playerCameraState;
    private IPlayerState _playerWeaponTypeState;

    // Set up property so that it invokes exit and enter upon state change
    internal IPlayerState PlayerState
    {
        get { return _playerState; }
        set { _playerState.End(); _playerState = value; _playerState.Begin(); }
    }

    internal IPlayerState PlayerCameraState
    {
        get { return _playerCameraState; }
        set { _playerCameraState.End(); _playerCameraState = value; _playerCameraState.Begin(); }
    }

    internal IPlayerState PlayerWeaponTypeState
    {
        get { return _playerWeaponTypeState; }
        set { _playerWeaponTypeState.End(); _playerWeaponTypeState = value; _playerWeaponTypeState.Begin(); }
    }

    // Use this for initialization
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        // Set beginning state
        _playerState = new JumpState();
        _playerState.Begin();

        //_playerCameraState = PlayerCameraStates.CameraRotateState;
        //_playerCameraState.Begin();

        _playerWeaponTypeState = new WeaponIdleState();
        _playerWeaponTypeState.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle input
        PlayerState.HandleInput();
        PlayerWeaponTypeState.HandleInput();

        // Change state if needed
        IPlayerState nextState = PlayerState.UpdateState();
        if (nextState != null)
        {
            PlayerState = nextState;
        }

        nextState = PlayerWeaponTypeState.UpdateState();
        if (nextState != null)
        {
            PlayerWeaponTypeState = nextState;
        }

        _characterController.Move(MoveDirection * Time.deltaTime);

        MoveDirection.x = 0.0f;
        MoveDirection.z = 0.0f;

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
