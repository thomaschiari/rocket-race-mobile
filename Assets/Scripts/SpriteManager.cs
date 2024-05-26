using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites; // Array de sprites
    public Sprite[] minerals; // Array de minerais

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

    public Sprite GetRandomMineral()
    {
        if (minerals.Length == 0)
        {
            Debug.LogWarning("No minerals available in SpriteManager!");
            return null;
        }
        int index = Random.Range(0, minerals.Length);
        return minerals[index];
    }
}