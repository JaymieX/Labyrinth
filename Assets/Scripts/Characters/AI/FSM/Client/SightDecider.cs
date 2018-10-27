using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Decider/SightDecider")]
public class SightDecider : MonsterDecider
{
    public override bool Condition(MonsterStateController msc)
    {
        return msc.SeekForward();
    }
}
