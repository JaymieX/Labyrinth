using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Decider/OutOfRangeDecider")]
public class OutOfRangeDecider : MonsterDecider
{
    public override bool Condition(MonsterStateController msc)
    {
        // Get both GO's location
        Vector3 playerPos = PlayerManager.Instance.PlayerCharacterController.transform.position;
        playerPos.y = 0f;

        Vector3 monsterPos = msc.transform.position;
        monsterPos.y = 0f;

        return Vector3.Distance(playerPos, monsterPos) > msc.MInfo.DeaggroDistance;
    }
}
