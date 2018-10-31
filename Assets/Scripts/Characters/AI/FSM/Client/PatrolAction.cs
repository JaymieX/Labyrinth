using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Action/PatrolAction")]
public class PatrolAction : MonsterAction
{
    public override void Execute(MonsterStateController msc)
    {
        msc.gameObject.GetComponent<MonsterPatrolController>().Partrol();
    }
}
