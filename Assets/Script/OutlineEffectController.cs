using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Use this script with a SpriteRenderer that has a material using custom sprite outline effect shader.
/// This script controls the color of the outline rendered by custom sprite outline effect shader.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class OutlineEffectController : MonoBehaviour
{
    public Color[] Colors;

    private Color _originalOutline;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalOutline = _spriteRenderer.material.GetColor("_OutColor");
    }

    public void SetOutlineColorToIndex(int i)
    {
        if(i >= Colors.Length) return;
        _spriteRenderer.material.SetColor("_OutColor", Colors[i]);
    }

    public void ResetOutlineColor()
    {
        _spriteRenderer.material.SetColor("_OutColor", _originalOutline);
    }
}
