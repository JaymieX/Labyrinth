using UnityEngine;

public abstract class PickupAction : ScriptableObject
{
    public abstract void Execute(PlayerController p);
}
