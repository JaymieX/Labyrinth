using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/State/SeekerState")]
public class SeekState : MonsterState
{
    public override void Begin(MonsterStateController msc)
    {
        // Begin the scream animation
        msc.Ani.SetBool("FoundPlayer", true);
    }

    public override void UpdateState(MonsterStateController msc)
    {
        // Don't check while monster is playing screaming animation
        if (msc.Ani.GetCurrentAnimatorStateInfo(0).tagHash != Animator.StringToHash("ZombieWalk")) return;

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
