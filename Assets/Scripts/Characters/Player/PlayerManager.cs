using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    internal static PlayerManager Instance = null;
    internal CharacterController PlayerCharacterController;
    internal PlayerController PlayerController;

    internal ushort MaxPlayers = 1; // Max number of player

    internal PlayerBehaviour[] PlayerBehaviours;

    internal ushort CurrentPlayer;

    // Global Resources
    public ushort Gears { get; set; }

    // Power up
    public float ModWalkSpeed { get; set; } // Mod walking speed
    public float ModHealth { get; set; } // Mod health
    public float ModLuck { get; set; } // Mod luck percentage
    public float ModGearRate { get; set; } // Mod money rate from loot

    // Misc
    public Vector3 PlayerPos;
    internal Vector3 MoveDirection = Vector3.zero;

    // Weapon
    internal ushort WeaponType;

    internal ushort CurRangeWeaponId;

    internal ushort CurMagSize;
    internal RangeWeaponInfo[] CurRangeWeaponInfo;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Instance.PlayerBehaviours = new PlayerBehaviour[MaxPlayers];
            Instance.Gears = 0;
            Instance.CurrentPlayer = 0;

            Instance.WeaponType = 0;

            Instance.CurRangeWeaponInfo = new RangeWeaponInfo[2];
            Instance.CurRangeWeaponInfo[0].Damage = 10.0f;
            Instance.CurRangeWeaponInfo[0].FireRate = 1.2f;
            Instance.CurRangeWeaponInfo[0].Range = 10.0f;
            Instance.CurRangeWeaponInfo[0].MaxAmmo = 8;
            Instance.CurRangeWeaponInfo[0].CurAmmo = 8;
            Instance.CurRangeWeaponInfo[0].ReloadTime = 3.2f;
        }

        DontDestroyOnLoad(this);
    }

    public PlayerBehaviour GetCurrentBehaviour()
    {
        return PlayerBehaviours[CurrentPlayer];
    }
}
