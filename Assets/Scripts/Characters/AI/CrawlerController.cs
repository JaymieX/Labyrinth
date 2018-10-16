using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour, IMonsterController
{
    private bool _inAttackRange;
    private bool _dead;
    private CharacterController _cc;
    private Animator _ani;

    // Use this for initialization
    void Start()
    {
        _inAttackRange = false;
        _dead = false;
        _cc = GetComponent<CharacterController>();
        _ani = GetComponent<Animator>();

        _ani.SetBool("FoundPlayer", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_dead) return;

        if (!_inAttackRange)
        {
            _ani.SetBool("PlayerInRange", false);

            LocalTrackPlayer();
        }
        else
        {
            Attack();
        }
    }

    public void GlobalTrackPlayer()
    {
        throw new System.NotImplementedException();
    }

    public void LocalTrackPlayer()
    {
        var offset = GetOffset();

        Quaternion rotation = Quaternion.LookRotation(offset);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.0f);

        if (offset.magnitude > 1.0f)
        {
            _cc.Move(offset * 0.2f * Time.deltaTime);
        }
        else
        {
            _inAttackRange = true;
        }
    }

    public void Attack()
    {
        _ani.SetBool("PlayerInRange", true);

        if (GetOffset().magnitude > 1.0f)
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
}
