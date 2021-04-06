using Interfaces.ObserverPattern;
using UnityEngine;

public class Player : Character, IObserver
{
    private bool _hasExtraJump;
    private ISubject _groundCheck;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpBehaviour = new HumanJump();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _groundCheck.RegisterObserver(this);
    }

    public override void PerformJump()
    {
        if (IsGrounded || _hasExtraJump)
        {
            _jumpBehaviour.Jump(_rigidbody, _jumpForce);
            _hasExtraJump = false;
        }
    }

    public void GetNotified()
    {
        _hasExtraJump = !IsGrounded;
    }
}