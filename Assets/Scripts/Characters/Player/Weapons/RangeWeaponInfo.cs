using UnityEngine;

[CreateAssetMenu(menuName = "Player/Data/RangeWeaponInfo")]
public class RangeWeaponInfo : ScriptableObject
{
    public float Range;
    public float FireRate;
    public float Damage;
    public float Spread;

    public ushort MaxAmmo;

    public float ReloadTime;
}
