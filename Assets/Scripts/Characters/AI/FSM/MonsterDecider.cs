using UnityEngine;

public abstract class MonsterDecider : ScriptableObject
{
    /// <summary>
    /// A condition to be tested
    /// The condition is retrieved from data with in a
    /// MonsterStateController
    /// </summary>
    /// <param name="msc">The monster state controller to be used</param>
    /// <returns>If said condition succeed return true other wise false</returns>
    public abstract bool Condition(MonsterStateController msc);
}
