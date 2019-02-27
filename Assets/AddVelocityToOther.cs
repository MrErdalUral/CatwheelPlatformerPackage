using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocityToOther : MonoBehaviour
{
    public float Velocity;
    public float Direction { get; set; }

    public void AddVelocity(GameObject other)
    {
        var rigidbody = other.GetComponent<Rigidbody2D>();
        if (rigidbody == null)
            rigidbody = other.AddComponent<Rigidbody2D>();
        rigidbody.velocity += Vector2.right * Velocity * Direction;

    }
}
