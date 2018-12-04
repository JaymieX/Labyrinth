using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/FastWalk")]
public class FastWalkPickupAction : PickupAction
{
    public override void Execute(PlayerController p)
    {
        p.FastWalk();
    }
}
