using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab; // Referência ao prefab do projetil
    public Transform firePoint; // Ponto de origem do disparo
    public static int mineralCount = 5; // Contador de minerais

    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Calcular os limites da tela
        Camera camera = Camera.main;
        Vector3 screenBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;
    }

    void Update()
    {
        // Movimento do foguete
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        transform.Translate(movement * speed * Time.deltaTime);

        // Restringir a posição do foguete dentro dos limites da tela
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;

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