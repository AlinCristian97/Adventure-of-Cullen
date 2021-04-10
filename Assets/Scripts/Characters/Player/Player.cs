using System;
using StrategyPattern.Behaviours;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character<PlayerBrain, PlayerActions, PlayerStats, PlayerComponents, PlayerReferences>
{
    // [SerializeField] private UnityEvent _onCharacterJump;

    // private void Awake()
    // {
    //     _jumpBehaviour = new HumanJump();
    // }
    
    // public override void PerformJump()
    // {
    //     base.PerformJump();
    //     _onCharacterJump?.Invoke();
    // }

    protected void Awake()
    {
        _brain = new PlayerBrain(this);
        _actions = new PlayerActions(this);
    }
    
    private void Update()
    {
        _brain.HandleDecisions();
    }

    private void FixedUpdate()
    {
        _actions.Move();
    }
}