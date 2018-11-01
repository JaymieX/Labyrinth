using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

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
    public ushort CurrentAmmo;

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
    internal List<RangeWeaponInfo> CurRangeWeaponInfo;

    /****************************************************
     *
     * Misc
     *
     ****************************************************/

    public Vector3 PlayerPos;
    internal Vector3 MoveDirection = Vector3.zero;

    internal bool IsInvincible;

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
            Instance.CurRangeWeaponInfo = new List<RangeWeaponInfo>();
        }

        // Setup so that Player manager do not gets destroyed
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // Listen for weapon switch
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CurRangeWeaponId = (ushort) ((CurRangeWeaponId + 1) % (CurRangeWeaponInfo.Count));
            CurrentAmmo = CurRangeWeaponInfo[CurRangeWeaponId].MaxAmmo;
        }
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

        IsInvincible = false;
    }

    /****************************************************
     *
     * Communication functions
     *
     ****************************************************/

    public void RemoveHealth(float amount)
    {
        if (IsInvincible) return; // Dont remove health if invincible

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
        PlayerController.gameObject.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("GameOverScene");
    }

    public void TriggerInvincible()
    {
        if (IsInvincible) return; // Dont trigger if player is already invincible

        StartCoroutine("InvincibleCountDown");
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

    private IEnumerator InvincibleCountDown()
    {
        float time = 10f;
        IsInvincible = true;

        while (time > 0f)
        {
            time -= 1f;
            yield return new WaitForSeconds(1f);
        }

        IsInvincible = false;
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
