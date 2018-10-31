using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Event/DeathEvent")]
public class DeathEvents : ScriptableObject
{
    public void OnDeathEnters(MonsterStateController msc)
    {
        // Disable death state transition
        // This is to prevent monster state controller revert back and calls death state repeatably
        msc.AllSateEnableList.Set(0, false);

        msc.Ani.SetTrigger("Dead");

        msc.Die();
    }
}
