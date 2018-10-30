using System;
using System.Collections;
using UnityEngine;

// Signature
internal delegate void HandlePlayerOpenInteract();

public class PlayerManager : MonoBehaviour
{
    /****************************************************
     *
     * Singleton
     *
     ****************************************************/
    internal static PlayerManager Instance = null;

    /****************************************************
     *
     * Inspector related objects
     *
     ****************************************************/

    internal PlayerController PlayerController;

    internal ushort MaxPlayers = 1; // Max number of player
    internal PlayerBehaviour[] PlayerBehaviours;
    internal ushort CurrentPlayer;

    /****************************************************
     *
     * Game data
     *
     ****************************************************/

    // Resources
    public ushort Gears { get; set; }

    public float CurrentHealth;

    // Power up
    public float ModWalkSpeed { get; set; } // Mod walking speed
    public float ModHealth { get; set; } // Mod health
    public float ModLuck { get; set; } // Mod luck percentage
    public float ModGearRate { get; set; } // Mod money rate from loot

    /****************************************************
     *
     * Weapon data
     * Weapon type:
     * 0 for range weapons
     * 1 for melee weapons
     *
     ****************************************************/
    internal ushort WeaponType;
    internal ushort CurRangeWeaponId;

    internal ushort CurMagSize;
    internal RangeWeaponInfo[] CurRangeWeaponInfo;

    /****************************************************
     *
     * Misc
     *
     ****************************************************/

    public Vector3 PlayerPos;
    internal Vector3 MoveDirection = Vector3.zero;

    /****************************************************
     *
     * Actions
     *
     ****************************************************/

    internal Action OnPlayerOpenInteract;

    void Awake()
    {
        if (Instance == null)
        {
            // Init singleton
            Instance = this;

            Instance.PlayerBehaviours = new PlayerBehaviour[MaxPlayers];
            Instance.Gears = 0;
            Instance.CurrentPlayer = 0;

            // Set current weapon type
            Instance.WeaponType = 0;

            // Init range weapon info
            Instance.CurRangeWeaponInfo = new RangeWeaponInfo[2];
            Instance.CurRangeWeaponInfo[0].Damage = 10.0f;
            Instance.CurRangeWeaponInfo[0].FireRate = 1.2f;
            Instance.CurRangeWeaponInfo[0].Range = 10.0f;
            Instance.CurRangeWeaponInfo[0].MaxAmmo = 8;
            Instance.CurRangeWeaponInfo[0].CurAmmo = 8;
            Instance.CurRangeWeaponInfo[0].ReloadTime = 3.2f;
        }

        // Setup so that Player manager do not gets destroyed
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // Constantly replenish health;
    }

    /****************************************************
     *
     * Reset related functions
     *
     ****************************************************/

    public void ResetAll()
    {
        ResetResources();
        StartCoroutine("RegenHealth");
    }

    public void ResetResources()
    {
        CurrentHealth = GetCurrentBehaviour().BaseHealth;

        ModWalkSpeed = 1f;
        ModHealth = 1f;
        ModLuck = 1f;
        ModGearRate = 1f;
    }

    /****************************************************
     *
     * Communication functions
     *
     ****************************************************/

    public void RemoveHealth(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth < 0)
        {
            OnPlayerDeath();
        }
    }

    /****************************************************
     *
     * Event functions
     *
     ****************************************************/

    private void OnPlayerDeath()
    {
        Debug.Log("You Died!");
    }

    private IEnumerator RegenHealth()
    {
        while (true)
        {
            CurrentHealth += 1f * ModHealth;
            if (CurrentHealth > GetCurrentBehaviour().BaseHealth)
            {
                CurrentHealth = GetCurrentBehaviour().BaseHealth;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    /****************************************************
     *
     * Helper functions
     *
     ****************************************************/

    public PlayerBehaviour GetCurrentBehaviour()
    {
        return PlayerBehaviours[CurrentPlayer];
    }
}
