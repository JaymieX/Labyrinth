using UnityEngine;

public abstract class TrapAction : ScriptableObject
{
    public TrapData Data;

    public abstract void OnTrapTriggered(Collider other, TrapController c);
}
