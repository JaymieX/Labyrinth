﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// A packed struct for state transition
/// </summary>
[System.Serializable]
public struct TransitionPack
{
    public MonsterDecider Decider;
    public MonsterState SucceedState;
    public MonsterState FailState;
}

/// <summary>
/// A struct for UnityEvent of MonsterStateController to show in inspector
/// </summary>
[System.Serializable]
public class StateEvent : UnityEvent<MonsterStateController>
{
};

[RequireComponent(typeof(NavMeshAgent), typeof(Collider), typeof(Animator))]
public class MonsterStateController : MonoBehaviour
{
    /****************************************************
     *
     * Inspector related objects
     *
     ****************************************************/

    // Inspector variables
    public MonsterInfo MInfo;

    // FSM
    public MonsterState CurrentMonsterState;
    public TransitionPack[] AllStateTransitions;

    internal BitArray AllSateEnableList;

    // GameObject components
    internal NavMeshAgent NavMA;
    internal Animator Ani;
    internal AudioSource Audio;

    // TargetPlayer
    internal PlayerController TargetPlayer;

    // Drops
    public GameObject[] DeathPickups;

    // Other
    public GameObject Projectile;
    private float _coolDown;

    /****************************************************
     *
     * Misc
     *
     ****************************************************/

    // Resources
    internal float Health;

    // Data
    internal float AttackInterval;
    internal float NavMeshUpdateInterval;
    internal bool IsDead;

    // Use this for initialization
    private void Start()
    {
        NavMA = GetComponent<NavMeshAgent>();
        Ani = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();

        Health = MInfo.Health;

        AttackInterval = 0f;
        NavMeshUpdateInterval = 1f;
        IsDead = false;

        // All state
        AllSateEnableList = new BitArray(AllStateTransitions.Length, true);
    }

    // Update is called once per frame
    private void Update()
    {
        // All state
        for (int i = 0; i < AllStateTransitions.Length; i++)
        {
            if (AllSateEnableList.Get(i))
            {
                ChangeState(AllStateTransitions[i].Decider.Condition(this)
                    ? AllStateTransitions[i].SucceedState
                    : AllStateTransitions[i].FailState);
            }
        }

        // Current state
        CurrentMonsterState.UpdateState(this);
    }

    /****************************************************
     *
     * Helper functions
     *
     ****************************************************/

    public void ChangeState(MonsterState newState)
    {
        if (newState == null) return; // Cancel change if newState is null

        // Change state
        CurrentMonsterState.End(this);
        CurrentMonsterState = newState;
        CurrentMonsterState.Begin(this);
    }

    public bool SeekForward()
    {
        RaycastHit hit;

        // Test to see if monster can see any thing in front
        if
        (
            Physics.SphereCast
            (
                transform.position + new Vector3(0f, 0.5f, 0f),
                MInfo.SightRad,
                transform.forward,
                out hit,
                MInfo.SightRange
            )
        )
        {
            // Test to see if monster saw player
            if (hit.transform.tag == "Player")
            {
                // Assign target player
                TargetPlayer = hit.transform.gameObject.GetComponent<PlayerController>();

                return true;
            }
        }

        return false; // Did not see any objects
    }

    public bool SeekAttackForward()
    {
        Collider[] hitColliders = Physics.OverlapSphere
        (
            transform.position + new Vector3(0f, 1f, MInfo.AttackRange),
            MInfo.AttackRad
        );
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                return true;
            }
        }

        return false; // Did not see any objects
    }

    public void ChasePlayer()
    {
        Vector3 playerPos = TargetPlayer.transform.position;
        playerPos.y = transform.position.y;

        NavMA.SetDestination(playerPos);
    }

    public void RemoveHealth(float amount)
    {
        Health -= amount;
    }

    public void Die()
    {
        GetComponent<Collider>().enabled = false;

        if (Random.Range(0f, 1f) < .6f)
        {
            if (DeathPickups.Length != 0)
            {
                Instantiate(DeathPickups[Random.Range(0, DeathPickups.Length)], transform.position + Vector3.up * 1.5f,
                    Quaternion.identity);
            }
        }

        StartCoroutine("Despawn");
    }

    private IEnumerator Despawn()
    {
        float counter = 0f;
        while (counter < 10f)
        {
            counter += 1f;
            yield return new WaitForSeconds(1f);
        }

        Destroy(this.gameObject);
    }

    public void FireProjectile()
    {
        if (_coolDown < 0f)
        {
            _coolDown = 0.1f;
            Instantiate(Projectile, transform.position + Vector3.up * 1.6f + transform.forward * 2f,
                transform.rotation);
        }
        else
        {
            _coolDown -= Time.deltaTime;
        }
    }
}
