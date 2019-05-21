using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;

public class TransformRight : MonoBehaviour
{
    public Vector2UnityEvent OutputTransformRight;
    // Update is called once per frame
    void Update()
    {
        OutputTransformRight.Invoke(transform.right);
    }
}
