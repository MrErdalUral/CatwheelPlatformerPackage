using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flash a sprite with a selected tint color using this component's Flash function.
/// 1. Add colors to the Colors array from the inspector
/// 2. Call Flash method with the index of a color on the Colors array
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ColorFlash : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Color[] Colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
    public float FlashTime = 0.05f;

    private Color _originalColor;
    private WaitForSeconds FlashForSeconds;
    // Start is called before the first frame update
    void Awake()
    {
        FlashForSeconds = new WaitForSeconds(FlashTime);
        _renderer = GetComponent<SpriteRenderer>();
        _originalColor = _renderer.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(FlashRoutine(Color.red));
        }
    }

    //Call this function to use this component
    public void Flash(int i)
    {
        if (i < Colors.Length)
            StartCoroutine(FlashRoutine(Colors[i]));
    }

    private IEnumerator FlashRoutine(Color color)
    {
        _renderer.color = color;
        yield return FlashForSeconds;
        _renderer.color = _originalColor;
    }
}


