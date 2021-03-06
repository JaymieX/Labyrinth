﻿using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /****************************************************
     *
     * Singleton
     *
     ****************************************************/
    internal static SpawnManager Instance = null;

    /****************************************************
     *
     * Inspector related objects
     *
     ****************************************************/

    public Spawner[] Spawners;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            // Init singleton
            Instance = this;
        }
    }

    public void SpawnEnemy(Vector3 player)
    {
        foreach (var spawner in Spawners)
        {
            Vector3 playerPos = player;
            playerPos.y = 0f;

            Vector3 spawnerPos = spawner.transform.position;
            spawnerPos.y = 0f;

            // Spawner within enable distance
            if (Vector3.Distance(playerPos, spawnerPos) <= spawner.EnableDistance)
            {
                spawner.Spawn();
            }
        }
    }
}
