using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/Key")]
public class KeyPickupAction : PickupAction
{
    public override void Execute(PlayerController p)
    {
        p.CanWinGame = true;
    }
}
