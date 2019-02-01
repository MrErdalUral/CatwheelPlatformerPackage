using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheckStatus2D : MonoBehaviour
{
    private bool _isTouchingGround;

    public BoolUnityEvent OutputIsMidair;

    public bool CanJump
    {
        get
        {
            var result = _isTouchingGround;
            return result;
        }
    }


    public bool IsTouchingGround
    {
        get
        {
            return _isTouchingGround;}
        set
        {
            _isTouchingGround = value;
            OutputIsMidair.Invoke(!_isTouchingGround);
        }
    }

    public void SetTouchingGround(bool value)
    {
        IsTouchingGround = value;
    }
}