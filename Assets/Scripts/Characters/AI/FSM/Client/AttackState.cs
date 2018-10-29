using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/State/AttackState")]
public class AttackState : MonsterState
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
        // Only execute based on attack speed
        if (msc.AttackInterval <= 0f)
        {
            msc.AttackInterval = msc.MInfo.AttackSpeed;
            // Execute
            foreach (var action in Actions)
            {
                action.Execute(msc);
            }
        }
        else
        {
            msc.AttackInterval -= Time.deltaTime;
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
