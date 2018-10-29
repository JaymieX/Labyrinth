using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Action/AttackAction")]
public class AttackAction : MonsterAction
{
    public override void Execute(MonsterStateController msc)
    {
        if (msc.SeekAttackForward())
        {
            Debug.Log("Monster hit player!");
        }
    }
}
