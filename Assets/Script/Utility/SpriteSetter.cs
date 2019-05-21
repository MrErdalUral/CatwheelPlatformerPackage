using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSetter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void SetLayer(SortingLayer layer, int orderInLayer = 0)
    {
        _spriteRenderer.sortingLayerID = layer.id;
        _spriteRenderer.sortingOrder = orderInLayer;
    }
}
