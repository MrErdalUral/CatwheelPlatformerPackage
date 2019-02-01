using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
/// <summary>
/// Use this script to get the direction of another object in the horizontal axis.
/// </summary>
public class OutputTaggedObjectHorizontalDirection : MonoBehaviour
{
    public string ObjectTag = "Player";
    public FloatUnityEvent OutputDirection;
    private GameObject _taggedObject;

    void Update()
    {
        if (!_taggedObject)
        {
            _taggedObject = GameObject.FindGameObjectWithTag(ObjectTag);
            return;
        }
    }

    public void Output(int scale)
    {
        if (!_taggedObject)
            return;

        OutputDirection.Invoke(Mathf.Sign(_taggedObject.transform.position.x - transform.position.x)* scale);
    }
}
