using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Event/AnimationEvent")]
public class AnimationEvents : ScriptableObject
{

    public void OnIdleBegin(MonsterStateController msc)
    {
        msc.Ani.SetBool("FoundPlayer", false);
    }

    public void OnAttackBegin(MonsterStateController msc)
    {
        msc.Ani.SetBool("PlayerInRange", true);
    }

    public void OnAttackEnd(MonsterStateController msc)
    {
        msc.Ani.SetBool("PlayerInRange", false);
    }

    public void OnDeathBegin(MonsterStateController msc)
    {
        msc.Ani.SetTrigger("Dead");
    }
}
