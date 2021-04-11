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

    private void Start()
    {
        Brain = new PlayerBrain(this);
        Actions = new PlayerActions(this);
        AddAnimations();
    }
    
    private void Update()
    {
        Brain.HandleDecisions();
    }

    private void FixedUpdate()
    {
        Actions.Move(transform);
    }

    private void AddAnimations()
    {
        AnyStateAnimation[] animations = {
            new AnyStateAnimation("Idle"),
            new AnyStateAnimation("Run")
        };
        
        Components.Animator.AddAnimations(animations);
    }
}