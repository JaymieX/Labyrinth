using UnityEngine;

[CreateAssetMenu(menuName = "Traps/Actions/GenericTrapAction")]
public class GenericTrapAction : TrapAction
{
    public override void OnTrapTriggered(Collider other, TrapController c)
    {
        if (c.Interval <= 0f)
        {
            c.Interval = Data.DamageInterval;

            //PlayerManager.Instance.RemoveHealth(Data.Damage);
            Debug.Log("Player lose hp");
        }
        else
        {
            c.Interval -= Time.deltaTime;
        }
    }
}
