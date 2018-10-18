using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour, IMonsterController
{
    private bool _inAttackRange;
    private bool _dead;
    private bool _trackPlayer;
    private CharacterController _cc;
    private Animator _ani;

    // Use this for initialization
    void Start()
    {
        _trackPlayer = false;
        _inAttackRange = false;
        _dead = false;
        _cc = GetComponent<CharacterController>();
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activate player tracking
        if (Input.GetKeyDown(KeyCode.H))
        {
            _trackPlayer = true;
            _ani.SetBool("FoundPlayer", true);
        }

        if (_dead) return;


        if (_trackPlayer)
        {
            LookTowardsPlayer();
            if (!_inAttackRange)
            {
                _ani.SetBool("PlayerInRange", false);

                if (_ani.GetCurrentAnimatorStateInfo(0).IsName("Zombie Walk"))
                {
                    LocalTrackPlayer();
                }
            }
            else
            {
                Attack();
            }
        }
    }

    public void GlobalTrackPlayer()
    {
        throw new System.NotImplementedException();
    }

    public void LocalTrackPlayer()
    {
        var offset = GetOffset();
        offset = new Vector3(offset.x, 0.0f, offset.z);

        LookTowardsPlayer();

        if (offset.magnitude > 1.0f)
        {
            _cc.SimpleMove(offset.normalized * 20.0f * Time.deltaTime);
        }
        else
        {
            _inAttackRange = true;
        }
    }

    public void Attack()
    {
        _ani.SetBool("PlayerInRange", true);

        var offset = GetOffset();
        offset = new Vector3(offset.x, 0.0f, offset.z);

        if (offset.magnitude > 1.0f)
        {
            _inAttackRange = false;
        }
    }

    public void Die()
    {
        if (_dead) return;

        _ani.SetTrigger("Dead");
        _dead = true;
    }

    private Vector3 GetOffset()
    {
        return PlayerManager.Instance.PlayerPos - transform.position;
    }

    private void LookTowardsPlayer()
    {
        var offset = GetOffset();
        offset = new Vector3(offset.x, 0.0f, offset.z);
        Quaternion rotation = Quaternion.LookRotation(offset);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.0f);
    }
}
