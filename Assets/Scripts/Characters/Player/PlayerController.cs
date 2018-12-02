using UnityEngine;

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

    internal float moveSpeed = 5.0f;
    internal float rotateSpeed = 100.0f;

    private GameObject _cameraObject;
    private CharacterController _controller;

    // Weapon related
    private float fireCoolDown = 0.2f;

    // Events
    internal PlayerOpenInteract OnPlayerOpenInteract;

    // Use this for initialization
    void Start()
    {
        _cameraObject = transform.GetChild(0).gameObject;

        _controller = GetComponent<CharacterController>();
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
                    }
                }
            }
        }
        else
        {
            // Decrease cool down
            fireCoolDown -= Time.deltaTime;
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
}
