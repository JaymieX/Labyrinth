using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNTrait : ICommand
{
    public void Execute()
    {
        RangeWeaponInfo rangeWeapon =
            PlayerManager.Instance.CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId];

        rangeWeapon.CurAmmo--;

        RaycastHit hit;
        if (PlayerManager.Instance.PlayerController.CastRay(rangeWeapon.Range, out hit))
        {
            if (hit.collider.tag == "Monster")
            {
                var monster = hit.collider.gameObject.GetComponent<MonsterStateController>();
                monster.RemoveHealth(rangeWeapon.Damage);
            }
        }
    }
}
