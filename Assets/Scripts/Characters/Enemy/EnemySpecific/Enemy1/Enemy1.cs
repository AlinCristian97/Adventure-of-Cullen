using System;
using FiniteStateMachine;
using UnityEngine;

public class Enemy1 : Entity
{
    public Enemy1IdleState IdleState { get; private set; }
    public Enemy1MoveState MoveState { get; private set; }

    [SerializeField] private IdleStateData _idleStateData;
    [SerializeField] private MoveStateData _moveStateData;

    public override void Awake()
    {
        base.Awake();

        MoveState = new Enemy1MoveState(this, StateMachine, "Move", _moveStateData, this);
    }
}