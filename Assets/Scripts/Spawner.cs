using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject mineralPrefab;
    public float asteroidSpawnRate = 5.5f;
    public float mineralSpawnRate = 10.0f;
    public float spawnRadius = 0.5f; // Define o raio para verificar a sobreposição

    private float asteroidTimer = 0f;
    private float mineralTimer = 0f;
    private float srTimer = 0f;
    private float spdTimer = 0f;
    public static float speed = 1f;

    void Update()
    {
        asteroidTimer += Time.deltaTime;
        mineralTimer += Time.deltaTime;
        srTimer += Time.deltaTime;
        spdTimer += Time.deltaTime;

        if (asteroidTimer >= asteroidSpawnRate)
        {
            SpawnObject(asteroidPrefab);
            asteroidTimer = 0f;
        }

        if (mineralTimer >= mineralSpawnRate)
        {
            SpawnObject(mineralPrefab);
            mineralTimer = 0f;
        }

        if (srTimer >= 5.0f && asteroidSpawnRate >= 0.5f)
        {
            asteroidSpawnRate *= 0.9f;
            srTimer = 0f;
        }
        
        if (spdTimer >= 5.0f && speed <= 5.0f)
        {
            speed *= 1.1f;
            spdTimer = 0f;
        }
    }

    void SpawnObject(GameObject prefab)
    {
        Vector3 spawnPosition;
        int attempts = 0;
        bool positionFound = false;

        do
        {
            spawnPosition = new Vector3(Random.Range(-2.5f, 2.5f), 5.5f, 0);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, spawnRadius);

            if (colliders.Length == 0)
            {
                positionFound = true;
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                Debug.Log(prefab.name + " spawned at " + spawnPosition);
            }

            attempts++;
        } while (!positionFound && attempts < 10);

        if (!positionFound)
        {
            Debug.LogWarning("Could not find a suitable spawn position for " + prefab.name);
        }
    }
}
