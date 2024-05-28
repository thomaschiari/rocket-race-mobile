using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryGameController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject mineralPrefab;
    public GameObject bossPrefab;
    public float asteroidSpawnRate;
    public float mineralSpawnRate;
    public float bossSpawnInterval; // Tempo em segundos para o Boss aparecer
    public float spawnRadius = 0.05f; // Define o raio para verificar a sobreposição
    private float asteroidTimer = 0f;
    private float mineralTimer = 0f;
    private float bossTimer = 0f; // Timer para controlar o spawn do Boss
    private float srTimer = 0f;
    private float spdTimer = 0f;
    public static float speed;
    private GameObject currentBoss;
    private int bossHitCount = 0; // Contador de tiros recebidos pelo Boss
    public TextMeshProUGUI bossHealthText; // Referência ao texto de vida do Boss
    private int bossHealth; // Vida do Boss
    private BaseRocketController baseRocketController;
    private int spawnOres;

    void Awake()
    {
        PlayerPrefs.SetInt("StoryMode", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        asteroidSpawnRate = 10f;
        mineralSpawnRate = 30f; 
        bossSpawnInterval = 60f;
        bossHealth = 15;
        baseRocketController = GameObject.FindWithTag("Player").GetComponent<BaseRocketController>();
        spawnOres = 5;
    }

    // Update is called once per frame
    void Update()
    {
        asteroidTimer += Time.deltaTime;
        mineralTimer += Time.deltaTime;
        bossTimer += Time.deltaTime; // Incrementar o timer do Boss
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

        // Lógica de instanciar o Boss
        if (bossTimer >= bossSpawnInterval)
        {
            if (currentBoss == null)
            {
                SpawnBoss(bossPrefab);
                bossHitCount = 0; // Resetar o contador de tiros
                UpdateBossHealthText();
                Debug.Log("Boss spawned");
            }
            bossTimer = 0f;
        }

        if (srTimer >= 5.0f && asteroidSpawnRate >= 0.25f)
        {
            asteroidSpawnRate *= 0.9f;
            srTimer = 0f;
        }
        
        if (spdTimer >= 5.0f && speed <= 10.0f)
        {
            speed *= 1.05f;
            spdTimer = 0f;
        }

        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] minerals = GameObject.FindGameObjectsWithTag("Mineral");
        GameObject[] boss = GameObject.FindGameObjectsWithTag("Boss");

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

        foreach (GameObject b in boss)
        {
            if (b.transform.position.y < -8f)
            {
                DestroyBoss(b);
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
            spawnPosition = new Vector3(Random.Range(-2.25f, 2.25f), 5.5f, 0);
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

    void SpawnBoss(GameObject bossPrefab)
    {
        Vector3 spawnPosition = new Vector3(0, 7.5f, 0);
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        Debug.Log(bossPrefab.name + " spawned at " + spawnPosition);
        currentBoss = GameObject.FindWithTag("Boss");
        UpdateBossHealthText();
    }

    void DestroyObject(GameObject obj)
    {
        Debug.Log(obj.name + " destroyed");
        Destroy(obj);
    }

    void DestroyBoss(GameObject obj)
    {
        Debug.Log(obj.name + " destroyed");
        Destroy(obj);
        currentBoss = null;
        bossHealthText.text = ""; // Limpar o texto de vida do Boss
        baseRocketController.Multiplier -= 0.25f;
    }

    // Método para registrar o hit no Boss
    public void RegisterBossHit()
    {
        if (currentBoss != null)
        {
            bossHitCount++;
            UpdateBossHealthText();
            if (bossHitCount >= bossHealth)
            {
                Destroy(currentBoss);
                currentBoss = null;
                Debug.Log("Boss destroyed");
                baseRocketController.Multiplier += 0.25f;
                // Instanciar 5 minerais ao destruir o Boss
                for (int i = 0; i < spawnOres; i++)
                {
                    SpawnObject(mineralPrefab);
                }
                bossHealthText.text = ""; // Limpar o texto de vida do Boss
                if (bossHealth <= 50)
                {
                    bossHealth += 5;
                }
                if (bossSpawnInterval >= 20.0f)
                {
                    bossSpawnInterval *= 0.9f;
                }
                if (spawnOres <= 10)
                {
                    spawnOres++;
                }
            }
        }
    }

    void UpdateBossHealthText()
    {
        if (currentBoss != null)
        {
            int remainingHealth = bossHealth - bossHitCount;
            bossHealthText.text = "Boss Health: " + remainingHealth + " / " +  bossHealth;
        }
    }
}
