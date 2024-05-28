using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomizeSprite : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            SpriteManager spriteManager = FindObjectOfType<SpriteManager>();
            if (spriteManager != null)
            {
                renderer.sprite = spriteManager.GetRandomBoss();
            }
            else
            {
                Debug.LogWarning("SpriteManager not found in the scene!");
            }
        }
    }
}
