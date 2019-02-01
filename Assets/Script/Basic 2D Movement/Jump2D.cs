using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump2D : MonoBehaviour
{
    public Vector2 JumpVector = new Vector2(0, 20);
    public UnityEvent OnJump;

    private float _originalGravityScale;
    private Rigidbody2D _rigidbody2D;
    private GroundCheckStatus2D _status;

    [SerializeField]
    private bool _input;

    private bool _startJump;


    void Awake()
    {
        _input = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _originalGravityScale = _rigidbody2D.gravityScale;
        _status = GetComponent<GroundCheckStatus2D>();
    }

    public void JumpInput(bool input)
    {
        _input = input;
    }

    void Update()
    {
        if (!_status.CanJump)
            _rigidbody2D.gravityScale = _input && _rigidbody2D.velocity.y > 0
                ? _originalGravityScale
                : 2 * _originalGravityScale;
        if (_input && _status.CanJump)
            _startJump = true;
    }

    void FixedUpdate()
    {
        if (_startJump)
        {
            var v = _rigidbody2D.velocity;
            v.y = JumpVector.y;
            _rigidbody2D.velocity = v;
            OnJump?.Invoke();
            _startJump = false;
        }
    }
    public void Log()
    {
        Debug.Log("Jump!");
    }
}