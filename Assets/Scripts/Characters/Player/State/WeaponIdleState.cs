using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdleState : IPlayerState
{
    private RangeWeaponInfo _weaponInfo;
    private IPlayerState _state;

    private float _fireInterval;

    public void Begin()
    {
        _state = null;

        _weaponInfo = PlayerManager.Instance.CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId];
        _weaponInfo.CurAmmo = _weaponInfo.MaxAmmo;

        _fireInterval = 0f;
    }

    public void End()
    {
    }

    public void HandleInput()
    {
        if (Input.GetButton("Fire1"))
        {
            ushort weaponType = PlayerManager.Instance.WeaponType;
            if (weaponType == 0) // Range
            {
                if (_fireInterval <= 0.1f)
                {
                    _fireInterval = _weaponInfo.FireRate;

                    RangeWeaponInfo info =
                        PlayerManager.Instance.CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId];

                    if (info.CurAmmo > 0)
                    {
                        PlayerManager.Instance.PlayerBehaviours[0].BaseRangeDamage.Execute();
                    }
                    else // No Ammo
                    {
                        _state = new WeaponReloadState();
                    }
                }
                else
                {
                    _fireInterval -= Time.deltaTime;
                }
            }
        }
    }

    public IPlayerState UpdateState()
    {
        return _state;
    }
}
