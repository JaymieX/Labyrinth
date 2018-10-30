using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/FSM/Event/DeathEvent")]
public class DeathEvents : ScriptableObject
{
    public void OnDeathEnters(MonsterStateController msc)
    {
        msc.Ani.SetTrigger("Dead");

        float time = 50f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
        }

        Destroy(msc.gameObject);
    }
}
