using System;
using StrategyPattern.Behaviours;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    [SerializeField] private UnityEvent _onCharacterJump;

    // private void Awake()
    // {
    //     _jumpBehaviour = new HumanJump();
    // }

    public override void PerformJump()
    {
        base.PerformJump();
        _onCharacterJump?.Invoke();
    }
}