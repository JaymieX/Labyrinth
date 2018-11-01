using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIdleState : IPlayerState
{
    private List<RangeWeaponInfo> _weaponInfo;
    private IPlayerState _state;

    private float _fireInterval;

    public void Begin()
    {
        _state = null;

        _weaponInfo = PlayerManager.Instance.CurRangeWeaponInfo;
        PlayerManager.Instance.CurrentAmmo = _weaponInfo[PlayerManager.Instance.CurRangeWeaponId].MaxAmmo;

        _fireInterval = 0f;
    }

    public void End()
    {
    }

    public void HandleInput()
    {
        Debug.Log(_fireInterval);
        if (Input.GetButton("Fire1"))
        {
            ushort weaponType = PlayerManager.Instance.WeaponType;
            if (weaponType == 0) // Range
            {
                if (_fireInterval <= 0.0f)
                {
                    _fireInterval = _weaponInfo[PlayerManager.Instance.CurRangeWeaponId].FireRate;

                    RangeWeaponInfo info =
                        PlayerManager.Instance.CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId];

                    if (PlayerManager.Instance.CurrentAmmo > 0)
                    {
                        PlayerManager.Instance.PlayerBehaviours[0].BaseRangeDamage.Execute();
                        Debug.Log("Fired Weapon");
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
