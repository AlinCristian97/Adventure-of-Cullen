using System;
using StrategyPattern;
using StrategyPattern.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected string _name;
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [SerializeField] protected float _jumpForce;
    [field: SerializeField] public bool IsGrounded { get; set; } = true;
    
    protected IJumpBehaviour _jumpBehaviour;

    //Change jump behaviour at runtime
    protected void SetJumpBehaviour(IJumpBehaviour jumpBehaviour)
    {
        _jumpBehaviour = jumpBehaviour;
    }
    
    public void PerformJump()
    {
        if (IsGrounded)
        {
            _jumpBehaviour.Jump(_rigidbody, _jumpForce);
        }
    }
}