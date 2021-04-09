using System;
using StrategyPattern;
using StrategyPattern.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public CharacterComponents Components { get; private set; }
    protected CharacterReferences _references;
    protected CharacterUtilities _utilities;
    [field: SerializeField] public CharacterStats Stats { get; private set; }
    protected CharacterActions _actions;

    [SerializeField] protected string _name;
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
            _jumpBehaviour.Jump(Components.Rigidbody, _jumpForce);
        }
    }

    private void Awake()
    {
        _actions = new CharacterActions(this);
        _utilities = new CharacterUtilities(this);
    }

    private void Update()
    {
        _utilities.HandleInput();
    }

    private void FixedUpdate()
    {
        _actions.Move(transform);
    }
}