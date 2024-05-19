using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    public GameObject mineralPrefab; // Referência ao prefab do mineral

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
            Destroy(other.gameObject); // Destroi o asteróide
            Destroy(gameObject); // Destroi o projetil
            // Spawnar um mineral na posição do asteróide destruído
            Instantiate(mineralPrefab, transform.position, Quaternion.identity);
        }
    }
}