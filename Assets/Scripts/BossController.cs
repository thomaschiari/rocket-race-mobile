using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private StoryGameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<StoryGameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            gameController.RegisterBossHit();
            Destroy(other.gameObject); // Destruir o projétil ao colidir com o Boss
        }
    }
}
