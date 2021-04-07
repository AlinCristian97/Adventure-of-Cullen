using System;
using StrategyPattern;
using StrategyPattern.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; protected set; }
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

    public virtual void PerformJump()
    {
        if (IsGrounded)
        {
            _jumpBehaviour.Jump(Rigidbody, _jumpForce);
        }
    }
}