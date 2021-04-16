using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    [SerializeField] private PlayerData _playerData;

    #endregion

    #region Components

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    #endregion

    #region Other Variables

    private Vector2 _workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
        
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace; // Can be removed?
    }

    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion
    
}