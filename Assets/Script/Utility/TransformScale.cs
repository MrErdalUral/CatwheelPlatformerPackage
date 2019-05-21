using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;

public class TransformScale : MonoBehaviour
{
    public FloatUnityEvent OutputXScale;

    // Update is called once per frame
    void Update()
    {
        OutputXScale.Invoke(transform.localScale.x);
    }
}
