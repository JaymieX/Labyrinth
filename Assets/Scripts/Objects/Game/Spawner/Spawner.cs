using UnityEngine;

public class Spawner : MonoBehaviour
{
    /****************************************************
     *
     * Singleton
     *
     ****************************************************/

    public GameObject Monster;
    public float SpawnRadius;
    public float EnableDistance;

    public void Spawn()
    {
        Vector3 spawnPosition = transform.position;

        // Randomize location within radius
        spawnPosition.x += Random.Range(SpawnRadius * -1f, SpawnRadius);
        spawnPosition.z += Random.Range(SpawnRadius * -1f, SpawnRadius);

        Instantiate(Monster, spawnPosition, Quaternion.identity);
    }
}
