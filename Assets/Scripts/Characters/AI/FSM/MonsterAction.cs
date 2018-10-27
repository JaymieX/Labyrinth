using UnityEngine;

public abstract class MonsterAction : ScriptableObject
{
    /// <summary>
    /// The action to be executed.
    /// Actions can be movements or attack for instance
    /// </summary>
    /// <param name="msc">The monster state controller to be used</param>
    public abstract void Execute(MonsterStateController msc);
}
