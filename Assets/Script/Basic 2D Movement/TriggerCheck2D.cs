using System.Collections.Generic;
using System.Diagnostics;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TriggerCheck2D : MonoBehaviour
{
    public Collider2DUnityEvent OnTriggerEnter;
    public Collider2DUnityEvent OnTriggerExit;

    public BoolUnityEvent OutputCheckCollisions;

    private bool _isTriggered;
    public bool IsTriggered
    {
        get => _isTriggered;
        set
        {
            _isTriggered = value;
            OutputCheckCollisions.Invoke(IsTriggered);
        }
    }
    [SerializeField]
    private List<GameObject> _listOfCollisions;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    void Awake()
    {
        _listOfCollisions = new List<GameObject>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
        }
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_listOfCollisions.Contains(collision.gameObject)) return;
        _listOfCollisions.Add(collision.gameObject);
        OnTriggerEnter.Invoke(collision);
        IsTriggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_listOfCollisions.Contains(collision.gameObject)) return;
        _listOfCollisions.Remove(collision.gameObject);
        OnTriggerExit.Invoke(collision);
        IsTriggered = _listOfCollisions.Count > 0;
    }
}