using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/Invinc")]
public class InvinciablePickupAction : PickupAction
{
    public override void Execute()
    {
        Debug.Log("Invinciable!!!");
    }
}
