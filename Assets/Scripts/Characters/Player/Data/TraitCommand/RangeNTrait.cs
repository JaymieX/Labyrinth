using UnityEngine;

public class RangeNTrait : ICommand
{
    public void Execute()
    {
        RangeWeaponInfo rangeWeapon =
            PlayerManager.Instance.CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId];

        PlayerManager.Instance.CurrentAmmo--;

        RaycastHit hit;
        if (PlayerManager.Instance.PlayerController.CastRay(rangeWeapon.Range, out hit))
        {
            if (hit.collider.tag == "Monster")
            {
                var monster = hit.collider.gameObject.GetComponent<MonsterStateController>();
                monster.RemoveHealth(rangeWeapon.Damage);

                GameObject.Instantiate(EffectBank.Instance.GetEffect("bullet_flesh"), hit.point, Quaternion.identity);
            }
            else
            {
                GameObject.Instantiate(EffectBank.Instance.GetEffect("bullet_sand"), hit.point, Quaternion.identity);
            }
        }
    }
}
