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

    public void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
