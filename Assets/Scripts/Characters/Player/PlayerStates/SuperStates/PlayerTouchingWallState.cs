using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingLedge;
    protected bool GrabInput;
    protected bool JumpInput;
    protected int InputX;
    protected int InputY;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate(); 

        InputX = Player.InputHandler.NormalizedInputX;
        InputY = Player.InputHandler.NormalizedInputY;
        GrabInput = Player.InputHandler.GrabInput;
        JumpInput = Player.InputHandler.JumpInput;

        if (JumpInput)
        {
            Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (IsGrounded && !GrabInput)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!IsTouchingWall || (InputX != Player.FacingDirection && !GrabInput))
        {
            StateMachine.ChangeState(Player.AirState);
        }
        else if (IsTouchingWall && !IsTouchingLedge)
        {
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
        IsTouchingWall = Player.CheckIfTouchingWall();
        IsTouchingLedge = Player.CheckIfTouchingLedge();

        if (IsTouchingWall && !IsTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
