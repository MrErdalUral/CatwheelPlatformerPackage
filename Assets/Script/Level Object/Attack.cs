using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    public EquipmentDataUnityEvent OnAttackStart;
    public EquipmentDataUnityEvent OnAttackContinue;
    public WeaponDataUnityEvent OnFrontHandAttack;
    public WeaponDataUnityEvent OnBackHandAttack;
    public EquipmentDataUnityEvent OnAttackEnd;

    private bool _attacking;
    private EquipmentData _currentEquipmentData;

    public void StartAttack(EquipmentData equipmentData)
    {
        if (_attacking)
        {
            ContinueAttack(equipmentData);
            return;
        }
        _attacking = true;
        _currentEquipmentData = equipmentData;
        OnAttackStart.Invoke(_currentEquipmentData);
    }

    private void ContinueAttack(EquipmentData equipmentData)
    {
        OnAttackContinue.Invoke(equipmentData);
    }

    public void InvokeFrontHandAttack()
    {
        if (!_attacking) return;
        var atkObj = GameObject.Instantiate(_currentEquipmentData.FrontHand.DamageObject1,
            transform.position + Vector3.right * transform.localScale.x * _currentEquipmentData.FrontHand.Range, Quaternion.identity,
            transform) as DamageHealth;
        atkObj.DamageStats.Knockback = _currentEquipmentData.FrontHand.Knockback;
        atkObj.DamageStats.AttackDamage = _currentEquipmentData.FrontHand.Damage;
        OnFrontHandAttack.Invoke(_currentEquipmentData.FrontHand);
    }
    public void InvokeBackHandAttack()
    {
        if (!_attacking) return;
        var atkObj = GameObject.Instantiate(_currentEquipmentData.BackHand.DamageObject1,
            transform.position + Vector3.right * transform.localScale.x * _currentEquipmentData.BackHand.Range, Quaternion.identity,
            transform) as DamageHealth;
        atkObj.DamageStats.Knockback = _currentEquipmentData.BackHand.Knockback;
        atkObj.DamageStats.AttackDamage = _currentEquipmentData.BackHand.Damage;
        OnBackHandAttack.Invoke(_currentEquipmentData.BackHand);
    }

    public void InvokeAttackEnd()
    {
        _attacking = false;
        OnAttackEnd.Invoke(_currentEquipmentData);
    }
}