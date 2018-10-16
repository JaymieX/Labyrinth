using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloadState : IPlayerState
{
    private RangeWeaponInfo _weaponInfo;
    private IPlayerState _state;
    private float _reloadTime;

    public void Begin()
    {
        Debug.Log("Reloading");
        _weaponInfo = PlayerManager.Instance.CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId];
        _state = null;

        _reloadTime = _weaponInfo.ReloadTime;
    }

    public void End()
    {
        Debug.Log("Finish Reloading");
    }

    public void HandleInput()
    {
    }

    public IPlayerState UpdateState()
    {
        if (_reloadTime > 0.2f)
        {
            _reloadTime -= Time.deltaTime;
        }
        else
        {
            _state = new WeaponIdleState();
        }

        return _state;
    }
}
