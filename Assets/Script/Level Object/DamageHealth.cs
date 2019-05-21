using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Use this script to send damage to another gameobject
/// </summary>
public class DamageHealth : MonoBehaviour
{
    public Collider2DUnityEvent OnDamageOther;
    [SerializeField]
    public Damage DamageStats = new Damage
    {
        Knockback = 250,
        AttackDamage = 1
    };
    public void Damage(Collider2D other)
    {
        var otherHealth = other.gameObject.GetComponent<Health>();
        if (!otherHealth)
        {
            Debug.Log("Other object does not have a health script attached");
            return;
        }

        DamageStats.KnockbackDirection = (other.transform.position - transform.position).normalized;
        otherHealth.TakeDamage(DamageStats);
        OnDamageOther.Invoke(other);
    }
}
[Serializable]
public struct Damage
{
    public float Knockback;
    [HideInInspector]
    public Vector2 KnockbackDirection;
    public float AttackDamage;
}