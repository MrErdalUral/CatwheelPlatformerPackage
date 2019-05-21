using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

public class AutopilotToTargetPosition : MonoBehaviour
{
    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private float _targetX;

    public float DeactivateSqrDistance = 1;

    public IntUnityEvent OutputDirection;
    public UnityEvent OnActivateAutopilot;
    public UnityEvent OnDeactivateAutopilot;

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            if (value)
                OnActivateAutopilot.Invoke();
            else
                OnDeactivateAutopilot.Invoke();

            _isActive = value;
        }
    }

    public float TargetX
    {
        get
        {
            return _targetX;
        }
        set
        {
            IsActive = true;
            _targetX = value;
        }
    }

    public void DeactivateAutopilot()
    {
        IsActive = false;
    }

    public void ActivateAutopilot()
    {
        IsActive = true;
    }

    public void SetTarget(Vector3 pos)
    {
        TargetX = pos.x;
    }

    void Update()
    {
        if(!IsActive) return;
        var distance = TargetX - transform.position.x;
        var dir = Mathf.Sign(distance);
        if (distance * distance < DeactivateSqrDistance)
        {
            IsActive = false;
            dir = 0;
        }
        OutputDirection.Invoke((int)dir);
    }
}
