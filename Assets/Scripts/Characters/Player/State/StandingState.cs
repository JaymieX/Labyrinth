using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : IPlayerState
{
    private PlayerController _player;
    private IPlayerState _state;

    public void Begin()
    {
        _player = PlayerManager.Instance.PlayerController;
        _state = null;
    }

    public void End()
    {
    }

    public void HandleInput()
    {
        // Request to walk
        _player.MoveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

        _player.transform.Rotate(0, Input.GetAxis("Horizontal") * 2.0f, 0);

        _player.MoveDirection = _player.transform.TransformDirection(_player.MoveDirection); // Move base on the roated diretction

        _player.MoveDirection *= PlayerManager.Instance.GetCurrentBehaviour().BaseWalkSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            _state = new JumpState();
        }
    }

    public IPlayerState UpdateState()
    {
        return _state;
    }
}
