using System.Collections;
using UnityEngine;
using UnityEngine.Events;

delegate void PlayerOpenInteract();

public class PlayerController : MonoBehaviour
{
    // Control keys
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Jump;
    public KeyCode Fire;

    // Stats
    public Vector3 SpawnPoint;

    internal ushort id = 1;
    internal float moveSpeed = 5.0f;
    internal float rotateSpeed = 100.0f;
    internal float health = 100.0f;
    internal ushort gears = 0;

    private bool invincible = false;

    internal float healthRegenCoolDown = 3.5f;

    private GameObject _cameraObject;
    private CharacterController _controller;
    private AudioSource _audio;

    // Weapon related
    private float fireCoolDown = 0.2f;

    // Events
    internal PlayerOpenInteract OnPlayerOpenInteract;

    // Unity events
    public UnityEvent OnDeathEvent;
    public UnityEvent OnAliveEvent;

    // Use this for initialization
    void Start()
    {
        _cameraObject = transform.GetChild(0).gameObject;

        _controller = GetComponent<CharacterController>();

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Master controls

        // Open Gate
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (OnPlayerOpenInteract != null)
            {
                OnPlayerOpenInteract();
                OnPlayerOpenInteract = null; // Making sure player don't over press
            }
        }

        // Camera look
        if (Input.GetKey(Left))
        {
            // Left
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(Right))
        {
            // Right
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        // Setup global move speed
        Vector3 globalMoveSpeed = Vector3.zero;

        // Forward/backward
        if (Input.GetKey(Up))
        {
            // Forward
            //_controller.Move(transform.TransformDirection(Vector3.forward) * moveSpeed);
            globalMoveSpeed = transform.TransformDirection(Vector3.forward) * moveSpeed;
        }
        else if (Input.GetKey(Down))
        {
            // Backward
            //_controller.Move(transform.TransformDirection(Vector3.back) * moveSpeed);
            globalMoveSpeed = transform.TransformDirection(Vector3.back) * moveSpeed;
        }

        // Jump
        if (Input.GetKeyDown(Jump) && _controller.isGrounded)
        {
            // Setup jump height
            globalMoveSpeed.y += 100.0f;
        }

        // Gravity
        globalMoveSpeed += Physics.gravity;

        // Moving the character
        _controller.Move(globalMoveSpeed * Time.deltaTime);

        // Weapon
        if (Input.GetKeyDown(Fire))
        {
            // Play gun audio
            _audio.Play();
        }

        // Check if gun is cool down
        if (fireCoolDown <= 0.0f)
        {
            // Reset fire cool down
            fireCoolDown = 0.2f;

            if (Input.GetKey(Fire))
            {
                RaycastHit info;
                if (CastRay(20.0f, out info))
                {
                    // Check if hit target is monster
                    if (info.collider.tag == "Monster")
                    {
                        info.collider.gameObject.GetComponent<MonsterStateController>().RemoveHealth(3.5f);
                        gears++;

                        // VFX blood
                        Instantiate(EffectBank.Instance.Effects[1].Effect, info.point, Quaternion.identity);
                    }
                    else
                    {
                        // VFX dirt
                        Instantiate(EffectBank.Instance.Effects[0].Effect, info.point, Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            // Decrease cool down
            fireCoolDown -= Time.deltaTime;
        }

        // Stops audio
        if (Input.GetKeyUp(Fire))
        {
            Debug.Log("Keyup");
            _audio.Stop();
        }

        // Re-gen health
        if (healthRegenCoolDown <= 0.0f)
        {
            // Reset health cool down
            healthRegenCoolDown = 3.5f;

            // Actual re-gen
            if (health < 100.0f)
            {
                health += 1f;
            }
            else
            {
                health = 100f;
            }
        }
        else
        {
            // Decrease cool down
            healthRegenCoolDown -= Time.deltaTime;
        }
    }

    public bool CastRay(float range, out RaycastHit info)
    {
        // Ray cast
        Vector3 origin = _cameraObject.transform.position;
        origin.y -= 0.35f;

        Debug.DrawRay(origin, _cameraObject.transform.forward * range, Color.red);
        Vector3 direction = _cameraObject.transform.forward;

        return Physics.Raycast(origin, direction * range, out info, range);
    }

    public void RemoveHealth(float amount)
    {
        // Check if player is invincible
        if (invincible) return;

        // Decrease health
        health -= amount;

        if (gears != 0)
        {
            gears--; // Decrease gears
        }

        // Check death
        if (health < 0.0f)
        {
            // Player died
            KillPlayer();
        }
    }

    public void Invinvible()
    {
        StartCoroutine("BeginInvincible");
    }

    private IEnumerator BeginInvincible()
    {
        invincible = true;

        // 8 Seconds of invincible
        float time = 8f;
        while (time > 0f)
        {
            time -= 1.0f;

            yield return new WaitForSeconds(1f);
        }

        invincible = false;
    }

    private void KillPlayer()
    {
        // Show death screen
        OnDeathEvent.Invoke();

        // Move player back to spawn
        transform.position = SpawnPoint;

        // Invoke respawn
        StartCoroutine("BeginRespawn");
    }

    private IEnumerator BeginRespawn()
    {
        float time = 5f;
        while (time > 0f)
        {
            time -= 1.0f;

            yield return new WaitForSeconds(1f);
        }

        health = 100f; // Reset health
        OnAliveEvent.Invoke(); // Remove death screen
    }

    public void FastWalk()
    {
        StartCoroutine("BeginFastWalk");
    }

    private IEnumerator BeginFastWalk()
    {
        moveSpeed = 15f;

        float time = 8f;
        while (time > 0f)
        {
            time -= 1f;

            yield return new WaitForSeconds(1f);
        }

        moveSpeed = 5f;
    }
}
