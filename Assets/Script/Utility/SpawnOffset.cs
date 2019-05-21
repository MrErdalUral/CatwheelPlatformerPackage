using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOffset : MonoBehaviour
{
    public Vector3 Offset;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position += Offset;
    }
}
