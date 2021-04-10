using System;
using StrategyPattern;
using StrategyPattern.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character<TBrain, TActions, TStats, TComponents, TReferences> : MonoBehaviour
    where TBrain : CharacterBrain
    where TActions : CharacterActions
    where TStats : CharacterStats
    where TComponents : CharacterComponents
    where TReferences : CharacterReferences
{
    protected TBrain _brain;
    protected TActions _actions;
    [field: SerializeField] public TStats Stats { get; protected set; }
    [field: SerializeField] public TComponents Components { get; protected set; }
    protected TReferences _references;

    
    // [SerializeField] protected float _jumpForce;
    // [field: SerializeField] public bool IsGrounded { get; set; } = true;
    //
    // protected IJumpBehaviour _jumpBehaviour;
    //
    //
    // //Change jump behaviour at runtime
    // protected void SetJumpBehaviour(IJumpBehaviour jumpBehaviour)
    // {
    //     _jumpBehaviour = jumpBehaviour;
    // }
    //
    // public virtual void PerformJump()
    // {
    //     if (IsGrounded)
    //     {
    //         _jumpBehaviour.Jump(Components.Rigidbody, _jumpForce);
    //     }
    // }
}