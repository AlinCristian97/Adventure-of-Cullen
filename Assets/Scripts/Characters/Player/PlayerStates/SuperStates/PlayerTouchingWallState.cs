using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{


    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Execute()
    {
        base.Execute();

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
        
        
        
        if (IsTouchingWall && !IsTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }
}