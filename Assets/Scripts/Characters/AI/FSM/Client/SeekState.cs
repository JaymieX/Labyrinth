using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/State/SeekerState")]
public class SeekState : MonsterState
{
    public override void Begin(MonsterStateController msc)
    {
        msc.NavMA.destination = msc.transform.position; // Stop the nav mesh agent in its place

        foreach (var beginEvent in BeginEvents)
        {
            beginEvent.Invoke(msc);
        }
    }

    public override void UpdateState(MonsterStateController msc)
    {
        // Don't check while monster is playing screaming animation
        if (msc.Ani.GetCurrentAnimatorStateInfo(0).tagHash != Animator.StringToHash("ZombieWalk")) return;

        if (msc.NavMeshUpdateInterval <= 0f)
        {
            msc.NavMeshUpdateInterval = 1f;
            // Execute
            foreach (var action in Actions)
            {
                action.Execute(msc);
            }
        }
        else
        {
            msc.NavMeshUpdateInterval -= Time.deltaTime;
        }

        // Decide
        foreach (var trans in Transitions)
        {
            msc.ChangeState(trans.Decider.Condition(msc) ? trans.SucceedState : trans.FailState);
        }
    }

    public override void End(MonsterStateController msc)
    {
        foreach (var endEvent in EndEvents)
        {
            endEvent.Invoke(msc);
        }
    }
}
