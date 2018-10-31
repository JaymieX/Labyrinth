using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Action/AttackAction")]
public class AttackAction : MonsterAction
{
    public override void Execute(MonsterStateController msc)
    {
        var lookRotation = Quaternion.LookRotation((PlayerManager.Instance.PlayerPos - msc.transform.position).normalized);
        lookRotation.y = 0f;
        msc.transform.rotation = Quaternion.Slerp(msc.transform.rotation, lookRotation, Time.deltaTime * 3f);

        if (msc.SeekAttackForward())
        {
            Debug.Log("Monster hit player!");
            PlayerManager.Instance.RemoveHealth(msc.MInfo.AttackPoint);
        }
    }
}
