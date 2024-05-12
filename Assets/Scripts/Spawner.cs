using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public GameObject mineralPrefab;
    public float spawnRate = 1f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            Instantiate(asteroidPrefab, new Vector3(Random.Range(-2.5f, 2.5f), 5.5f, 0), Quaternion.identity);
            Debug.Log("Asteroid spawned");
            Instantiate(mineralPrefab, new Vector3(Random.Range(-2.5f, 2.5f), 5.5f, 0), Quaternion.identity);
            Debug.Log("Mineral spawned");
            timer = 0f;
        }
    }
}
