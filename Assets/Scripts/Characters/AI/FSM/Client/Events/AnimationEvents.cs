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

    public void OnSeekBegin(MonsterStateController msc)
    {
        // Begin the scream animation
        msc.Ani.SetBool("FoundPlayer", true);

        // Play roar sound
        msc.Audio.Play();
    }
}
