using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float angleOffset = 15f; // Ângulo dos projéteis diagonais
    public GameObject projectilePrefab; // Prefab do projétil

    void Start()
    {
        FireProjectiles();
        Destroy(gameObject, lifetime);
    }

    void FireProjectiles()
    {
        // Disparar o projétil principal
        InstantiateProjectile(Vector2.up);

        // Calcular as direções dos projéteis diagonais
        Vector2 directionLeft = Quaternion.Euler(0, 0, angleOffset) * Vector2.up;
        Vector2 directionRight = Quaternion.Euler(0, 0, -angleOffset) * Vector2.up;

        // Disparar os projéteis diagonais
        InstantiateProjectile(directionLeft);
        InstantiateProjectile(directionRight);
    }

    void InstantiateProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileMovement>().SetDirection(direction);
    }
}
