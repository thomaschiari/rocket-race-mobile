using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketController : BaseRocketController
{
    public float speed = 100f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float minX, maxX, minY, maxY;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        // Calcular os limites da tela
        Camera camera = Camera.main;
        Vector3 screenBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;
    }

    protected override void Update()
    {
        base.Update();
        Vector2 move = movement * speed * Time.deltaTime;
        rb.velocity = move;
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }

    public void MoveUp() { movement = Vector2.up; }
    public void MoveDown() { movement = Vector2.down; }
    public void MoveLeft() { movement = Vector2.left; }
    public void MoveRight() { movement = Vector2.right; }
    public void StopMovement() { movement = Vector2.zero; }

    public void Fire()
    {
        if (MineralCount > 0)
        {
            AudioManager.instance.PlayShoot();
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            MineralCount--;
        }
    }
}
