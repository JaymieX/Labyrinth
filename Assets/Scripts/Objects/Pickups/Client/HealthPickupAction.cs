using UnityEngine;

[CreateAssetMenu(menuName = "Pickupable/Action/Health")]
public class HealthPickupAction : PickupAction
{
    public override void Execute(PlayerController p)
    {
        var hp = p.health + 50f; // add 50 health to player

        // Trim
        if (hp > 100f)
        {
            hp = 100;
        }

        p.health = hp;
    }
}
