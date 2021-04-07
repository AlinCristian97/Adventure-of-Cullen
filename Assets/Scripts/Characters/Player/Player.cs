using StrategyPattern.Behaviours;
using UnityEngine;

public class Player : Character
{
    private int _numberOfJumps = 2;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpBehaviour = new HumanJump();
    }

    public override void PerformJump()
    {
        if (IsGrounded || _numberOfJumps > 0)
        {
            _jumpBehaviour.Jump(_rigidbody, _jumpForce);
            _numberOfJumps--;
        }
    }

    public void ResetNumberOfJumps()
    {
        _numberOfJumps = 2;
    }
}