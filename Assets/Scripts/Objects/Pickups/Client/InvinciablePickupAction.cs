using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/Invinc")]
public class InvinciablePickupAction : PickupAction
{
    public override void Execute(PlayerController p)
    {
        Debug.Log("Invincible!!!");
        p.Invinvible();
    }
}
