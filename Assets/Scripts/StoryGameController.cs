using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject mineralPrefab;
    public GameObject bossPrefab;
    public float asteroidSpawnRate;
    public float mineralSpawnRate;
    public float spawnRadius = 0.05f; // Define o raio para verificar a sobreposição
    private float asteroidTimer = 0f;
    private float mineralTimer = 0f;
    private float srTimer = 0f;
    private float spdTimer = 0f;
    public static float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        asteroidSpawnRate = 10f;
        mineralSpawnRate = 60f; 
    }

    // Update is called once per frame
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

        if (srTimer >= 5.0f && asteroidSpawnRate >= 0.25f)
        {
            asteroidSpawnRate *= 0.8f;
            srTimer = 0f;
        }
        
        if (spdTimer >= 5.0f && speed <= 10.0f)
        {
            speed *= 1.1f;
            spdTimer = 0f;
        }

        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] minerals = GameObject.FindGameObjectsWithTag("Mineral");

        foreach (GameObject asteroid in asteroids)
        {
            if (asteroid.transform.position.y < -5.5f)
            {
                DestroyObject(asteroid);
            }
        }

        foreach (GameObject mineral in minerals)
        {
            if (mineral.transform.position.y < -5.5f)
            {
                DestroyObject(mineral);
            }
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

    void DestroyObject(GameObject obj)
    {
        Debug.Log(obj.name + " destroyed");
        Destroy(obj);
    }
}
