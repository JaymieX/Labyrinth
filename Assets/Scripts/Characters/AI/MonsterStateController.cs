using UnityEngine;
using UnityEngine.AI;

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

    // GameObject components
    internal NavMeshAgent NavMA;
    internal Animator Ani;

    // Use this for initialization
    private void Start()
    {
        NavMA = GetComponent<NavMeshAgent>();
        Ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        CurrentMonsterState.UpdateState(this);
    }

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
                transform.position + new Vector3(0f, 1f, 0f),
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
}
