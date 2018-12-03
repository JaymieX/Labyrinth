using UnityEngine;

[CreateAssetMenu(menuName = "Traps/Actions/GenericTrapAction")]
public class GenericTrapAction : TrapAction
{
    public override void OnTrapTriggered(Collider other, TrapController c)
    {
        if (c.Interval <= 0f)
        {
            c.Interval = Data.DamageInterval;

            Debug.Log("Player lose hp");
            other.transform.gameObject.GetComponent<PlayerController>().RemoveHealth(Data.Damage);
        }
        else
        {
            c.Interval -= Time.deltaTime;
        }
    }
}
