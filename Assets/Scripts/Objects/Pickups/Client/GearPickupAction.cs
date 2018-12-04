using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/Gear")]
public class GearPickupAction : PickupAction
{
    public override void Execute(PlayerController p)
    {
        p.gears += 50; // Add 50 gears
    }
}
