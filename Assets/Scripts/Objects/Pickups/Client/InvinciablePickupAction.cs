using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/Invinc")]
public class InvinciablePickupAction : PickupAction
{
    public override void Execute()
    {
        //PlayerManager.Instance.TriggerInvincible();
        Debug.Log("Invincible!!!");
    }
}
