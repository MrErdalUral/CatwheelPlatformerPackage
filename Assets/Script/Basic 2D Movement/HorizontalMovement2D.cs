using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement2D : MonoBehaviour
{

    public float MaxSpeed = 10;
    public float Acceleration = 5;
    public bool FaceDirection = true;
    public bool InvertFaceDirection = false;

    private int _direction;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _originalScale;

    public FloatUnityEvent OutputDirection;

    public FloatUnityEvent OnChangeDirection;

    public int Direction
    {
        get => _direction;
        set
        {
            if ((int)Mathf.Sign(_direction) != (int)Mathf.Sign(value))
                OnChangeDirection.Invoke(value);
            _direction = value;
        }
    }

    void Update()
    {
        OutputDirection.Invoke(_rigidbody2D.velocity.normalized.x);
        if (FaceDirection && _direction != 0)
        {
            var sign = Mathf.Sign(_direction);
            if (InvertFaceDirection)
                sign = -sign;
            transform.localScale =  new Vector3(_originalScale.x*sign, _originalScale.y, _originalScale.z);
        }
    }

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _originalScale = transform.localScale;
    }

    public void SetDirection(float dir)
    {
        Direction = Mathf.RoundToInt(dir);
    }

    
    void FixedUpdate()
    {

        var targetVelocity = Direction * MaxSpeed;
        var timedBasedAcceleration = Time.deltaTime * Acceleration;
        if (timedBasedAcceleration > 1) timedBasedAcceleration = 1;
        var velocity = timedBasedAcceleration * targetVelocity + (1 - timedBasedAcceleration) * _rigidbody2D.velocity.x;
        _rigidbody2D.velocity = new Vector2(velocity, _rigidbody2D.velocity.y);
    }
}