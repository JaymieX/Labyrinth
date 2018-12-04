using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/State/GenericState")]
public class GenericState : MonsterState
{
    public override void Begin(MonsterStateController msc)
    {
        if (msc.NavMA.enabled)
        {
            msc.NavMA.destination = msc.transform.position; // Stop the nav mesh agent in its place
        }

        foreach (var beginEvent in BeginEvents)
        {
            beginEvent.Invoke(msc);
        }
    }

    public override void UpdateState(MonsterStateController msc)
    {
        // Execute
        foreach (var action in Actions)
        {
            action.Execute(msc);
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
