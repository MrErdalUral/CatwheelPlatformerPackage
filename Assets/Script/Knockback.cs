using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this script to add knockback effect when taking damage.
/// </summary>
public class Knockback : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamageKnockback(Damage damage)
    {
        _rigidbody2D.velocity = damage.Knockback *  damage.KnockbackDirection;
    }
}
