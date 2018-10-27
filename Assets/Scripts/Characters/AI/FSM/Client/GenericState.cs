using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/State/GenericState")]
public class GenericState : MonsterState
{
    public override void Begin(MonsterStateController msc)
    {
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
    }
}
