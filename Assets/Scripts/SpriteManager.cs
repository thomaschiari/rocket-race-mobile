using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites; // Coloca planet sprites aqui, attach this to some object in the scene

    public Sprite GetRandomSprite()
    {
        int index = Random.Range(0, sprites.Length);
        return sprites[index];
    }
}