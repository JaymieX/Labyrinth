using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/State/SeekerState")]
public class SeekState : MonsterState
{
    private bool _startChase;

    public override void Begin(MonsterStateController msc)
    {
        // Begin the scream animation
        msc.Ani.SetBool("FoundPlayer", true);
        _startChase = false;
    }

    public override void UpdateState(MonsterStateController msc)
    {
        _startChase = msc.Ani.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("ZombieWalk");

        // Don't check while monster is playing screaming animation
        if (!_startChase) return;

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
