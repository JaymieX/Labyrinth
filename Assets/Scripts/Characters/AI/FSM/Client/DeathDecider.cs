using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Decider/DeathDecider")]
public class DeathDecider : MonsterDecider
{
    public override bool Condition(MonsterStateController msc)
    {
        return msc.IsDead;
    }
}
