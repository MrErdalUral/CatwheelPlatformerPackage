using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Use this script to control and keep track of the health of gameobjects.
/// </summary>
public class Health : MonoBehaviour
{
    public float MaxHealth = 10;
    [SerializeField] private HealthObject _currentHealthObject;

    public FloatUnityEvent OnHealthChanged;
    public DamageUnityEvent OnTakeDamage;
    public FloatUnityEvent OnHealDamage;
    public float DeathTime = 1f;
    public UnityEvent OnDeath;
    private float _takeDamageCooldown;
    private WaitForSeconds _deathTime;

    public float CurrentHealth
    {
        get => _currentHealthObject.Health;
        set
        {
            if (_currentHealthObject.Health > MaxHealth)
                _currentHealthObject.Health = MaxHealth;
            OnHealthChanged.Invoke(CurrentHealth);
            if (_currentHealthObject.Health <= 0)
                OnDeath.Invoke();
            _currentHealthObject.Health = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _deathTime = new WaitForSeconds(DeathTime);
        if (!_currentHealthObject)
            _currentHealthObject = ScriptableObject.CreateInstance<HealthObject>();
        _currentHealthObject.Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _takeDamageCooldown -= Time.deltaTime;
    }

    public void TakeDamage(Damage damageHealth)
    {
        if (_takeDamageCooldown > 0) return;
        Debug.Log(gameObject.tag + " Health - " + damageHealth.AttackDamage);
        OnTakeDamage.Invoke(damageHealth);
        _takeDamageCooldown = 0.1f;
        CurrentHealth -= damageHealth.AttackDamage;
    }

    public void HealDamage(float heal)
    {
        CurrentHealth += heal;
        OnHealDamage.Invoke(heal);
    }
}