using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Action/GunBotAttackAction")]
public class GunBotAttackAction : MonsterAction
{
    public override void Execute(MonsterStateController msc)
    {
        var lookRotation = Quaternion.LookRotation((msc.TargetPlayer.transform.position - msc.transform.position).normalized);
        lookRotation.x = 0f;
        lookRotation.z = 0f;
        msc.transform.rotation = Quaternion.Slerp(msc.transform.rotation, lookRotation, Time.deltaTime * 3f);

        msc.FireProjectile();
    }
}
