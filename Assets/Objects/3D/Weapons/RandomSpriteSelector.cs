using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteSelector : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Count > 0)
        {
            int randomIndex = Random.Range(0, sprites.Count);

            spriteRenderer.sprite = sprites[randomIndex];
        }
        else
        {
            Debug.LogWarning("Список спрайтов пуст!");
        }
    }
}