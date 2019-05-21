using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : MonoBehaviour
{
    private int _originalLayer;
    public string Layer = "Ghost";

    void Awake()
    {
        _originalLayer = gameObject.layer;
    }

    public void SetGhost()
    {
        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ghost");
    }
    public void SetGhost(float time)
    {
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        StartCoroutine(ResetLayerRoutine(time));
    }

    public void SetCurrentLayer()
    {
        _originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer(Layer);
    }
    public void SetCurrentLayer(float time)
    {
        gameObject.layer = LayerMask.NameToLayer(Layer);
        StartCoroutine(ResetLayerRoutine(time));
    }

    private IEnumerator ResetLayerRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.layer = _originalLayer;
    }
}
