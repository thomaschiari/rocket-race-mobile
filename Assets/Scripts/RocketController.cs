using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab; // Referência ao prefab do projetil
    public Transform firePoint; // Ponto de origem do disparo
    public static int mineralCount = 5; // Contador de minerais

    void Update()
    {
        // Movimento do foguete
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        transform.Translate(movement * speed * Time.deltaTime);

        // Disparar projéteis
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mineralCount > 0)
            {
                Fire();
                mineralCount--;
            }
        }
    }

    void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}