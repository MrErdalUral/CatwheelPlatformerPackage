using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this component to control animator parameters
/// Define more function as you need them 
/// </summary>
public class AnimatorParameterController : MonoBehaviour
{
    private Animator _animator;

    public string AttackTrigger = "Attack";
    public string AttackContinueTrigger = "Attack Continue";

    public int AttackType
    {
        get => _animator.GetInteger("AttackType");
        set => _animator.SetInteger("AttackType", value);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDirectionParameter(float direction)
    {
        _animator.SetFloat("Direction", direction);
    }

    public void SetJumpingParameter(bool isJumping)
    {
        _animator.SetBool("Jumping", isJumping);

    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    public void SetAttackContinueTrigger()
    {
        _animator.SetTrigger(AttackContinueTrigger);
    }

    public void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void SetCanTransitionToJumpAttackToTrue()
    {
        _animator.SetBool("CanTransitionToJumpAttack", true);
    }
    public void SetCanTransitionToJumpAttackToFalse()
    {
        _animator.SetBool("CanTransitionToJumpAttack", false);
    }
}
