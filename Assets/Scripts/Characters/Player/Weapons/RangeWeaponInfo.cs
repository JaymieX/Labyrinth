using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RangeWeaponInfo
{
    public float Range { get; set; }
    public float FireRate { get; set; }
    public float Damage { get; set; }

    public ushort MaxAmmo { get; set; }
    public ushort CurAmmo { get; set; }

    public float ReloadTime { get; set; }
}
