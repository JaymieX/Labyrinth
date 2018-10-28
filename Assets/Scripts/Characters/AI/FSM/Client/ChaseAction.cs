using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Action/ChaseAction")]
public class ChaseAction : MonsterAction
{
    public override void Execute(MonsterStateController msc)
    {
        Debug.Log("Chase!");
        msc.ChasePlayer();
    }
}
