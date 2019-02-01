using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this script to send damage to another gameobject
/// </summary>
public class DamageHealth : MonoBehaviour
{
    public float Knockback = 250;
    public float AttackDamage = 1;
    public void Damage(Collider2D other)
    {
        var otherHealth = other.gameObject.GetComponent<Health>();
        if (!otherHealth)
        {
            Debug.Log("Other object does not have a health script attached");
            return;
        }
        otherHealth.TakeDamage(this);
    }
}
