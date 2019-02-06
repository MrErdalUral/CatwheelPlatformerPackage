using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicLerpMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPosition;
    private float _movingProgress;
    private Vector3 _startPos;

    public float MoveTime = 1f;

    public Vector3 TargetPosition
    {
        get { return _targetPosition; }
        set
        {
            _startPos = transform.position;
            _movingProgress = 0;
            _targetPosition = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _movingProgress += Time.deltaTime / MoveTime;
        transform.position = Vector3.Lerp(_startPos, TargetPosition, _movingProgress);
    }

    public void SetTargetPosition2D(Vector2 targetPos)
    {
        TargetPosition = (Vector3)targetPos + transform.position.y * Vector3.forward;
    }

    void Awake()
    {
        _startPos = _targetPosition = transform.position;
    }
}
