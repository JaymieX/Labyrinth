using UnityEngine;

public class Spawner : MonoBehaviour
{
    /****************************************************
     *
     * Singleton
     *
     ****************************************************/

    public GameObject Monster;
    public ushort Amount;
    public float SpawnRadius;
    public float EnableDistance;

    public void Spawn()
    {
        for (int i = 0; i < Amount; i++)
        {
            Vector3 spawnPosition = transform.position;

            // Randomize location within radius
            spawnPosition.x += Random.Range(SpawnRadius * -1f, SpawnRadius);
            spawnPosition.z += Random.Range(SpawnRadius * -1f, SpawnRadius);

            Instantiate(Monster, spawnPosition, Random.rotation);
        }
    }
}
