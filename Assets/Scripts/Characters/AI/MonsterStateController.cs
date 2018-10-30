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

    // GameObject components
    internal NavMeshAgent NavMA;
    internal Animator Ani;

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

        Health = MInfo.Health;

        AttackInterval = 0f;
        NavMeshUpdateInterval = 1f;
        IsDead = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // All state
        foreach (var allStateTransition in AllStateTransitions)
        {
            ChangeState(allStateTransition.Decider.Condition(this) ? allStateTransition.SucceedState : allStateTransition.FailState);
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
            return hit.transform.tag == "Player";
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
        Vector3 playerPos = PlayerManager.Instance.PlayerCharacterController.transform.position;
        playerPos.y = transform.position.y;

        NavMA.SetDestination(playerPos);
    }

    public void RemoveHealth(float amount)
    {
        Health -= amount;
    }
}
