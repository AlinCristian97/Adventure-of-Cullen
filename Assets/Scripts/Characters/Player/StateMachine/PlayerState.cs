using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    // Keep?
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    
    // All Checks in one place
        //-input
    protected int InputX;
    protected int InputY;
    protected bool GrabInput;
    protected bool JumpInput;

        //-surroundings
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingLedge;
    protected bool IsTouchingCeiling;

        //-state
    protected bool IsHanging;
    protected bool IsClimbing;

    // Keep?
    protected float StartTime; // used for coyote effect
    protected bool IsAnimationFinished;
    private string _animatorBoolName;
    protected bool IsExitingState; //TODO: Find another solution?

    // Initialize them in ctor?
    public PlayerState(Player player)
    {
        Player = player;
        StateMachine = Player.StateMachine;
    }

    public virtual void Enter()
    {
        CheckInput(); //TODO: Can be deleted?
        CheckSurroundings(); //TODO: Can be deleted?
        
        StartTime = Time.time;
        
        Player.Components.Animator.SetBool(_animatorBoolName, true);
        
        IsAnimationFinished = false;
        IsExitingState = false;
        
        //test
        Debug.Log(this);
    }

    public virtual void Exit()
    {
        Player.Components.Animator.SetBool(_animatorBoolName, false);
        
        IsExitingState = true;
    }
    
    public virtual void Execute()
    {
        CheckInput();
    }
    
    public virtual void ExecutePhysics()
    {
        CheckSurroundings();
    }

    private void CheckInput()
    {
        InputX = Player.InputHandler.NormalizedInputX;
        InputY = Player.InputHandler.NormalizedInputY;
        GrabInput = Player.InputHandler.GrabInput;
        JumpInput = Player.InputHandler.JumpInput;
    }

    private void CheckSurroundings()
    {
        IsGrounded = Player.CheckIfGrounded();
        IsTouchingWall = Player.CheckIfTouchingWall();
        IsTouchingLedge = Player.CheckIfTouchingLedge();
        IsTouchingCeiling = Player.CheckForCeiling();
    }

    //Keep them?
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;
}