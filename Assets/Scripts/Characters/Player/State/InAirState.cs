using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : IPlayerState
{
    private PlayerController _player;

    public virtual void Begin()
    {
        _player = PlayerManager.Instance.PlayerController;
    }

    public void End()
    {
    }

    public void HandleInput()
    {
    }

    public IPlayerState UpdateState()
    {
        // Apply grav
        _player.MoveDirection.y += -9.8f * Time.deltaTime;

        if (PlayerManager.Instance.PlayerCharacterController.isGrounded)
        {
            return new StandingState();
        }

        return null;
    }
}
