using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites; // Array de sprites

    public Sprite GetRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites available in SpriteManager!");
            return null;
        }
        int index = Random.Range(0, sprites.Length);
        return sprites[index];
    }
}