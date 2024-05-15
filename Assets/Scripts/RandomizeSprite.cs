using System.Collections;
using System.Collections.Generic;
uusing UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    // Colocar no asteroid / planet prefab
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            SpriteManager spriteManager = FindObjectOfType<SpriteManager>();
            renderer.sprite = spriteManager.GetRandomSprite();
        }
    }
}
