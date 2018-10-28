using UnityEngine;

[CreateAssetMenu(menuName = "MonsterFSM/Data/MonsterInfo")]
public class MonsterInfo : ScriptableObject
{
    // Combat stats
    public ushort Health;
    public float AttackPoint;
    public float AttackSpeed;
    public float AttackRange;

    // General stats
    public float WalkSpeed;
    public float DeaggroDistance;

    // Sphere cast info
    public float SightRange;
    public float SightRad;
}
