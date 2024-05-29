using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public GameObject mineralPrefab; // Referência ao prefab do mineral
    public GameObject mineralStoryPrefab; // Referência ao prefab do mineral na história

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            AudioManager.instance.PlayHit();
            Destroy(other.gameObject); // Destroi o asteróide
            Destroy(gameObject); // Destroi o projetil
            // Spawnar um mineral na posição do asteróide destruído
            if (PlayerPrefs.GetInt("StoryMode") == 1)
            {
                Instantiate(mineralStoryPrefab, other.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(mineralPrefab, other.transform.position, Quaternion.identity);
            }
        }
    }
}