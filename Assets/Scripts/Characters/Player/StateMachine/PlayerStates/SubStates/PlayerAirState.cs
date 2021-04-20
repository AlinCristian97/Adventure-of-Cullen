using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player) : base(player)
    {
    }

    public override void Execute()
    {
        base.Execute();

        InputX = Player.InputHandler.NormalizedInputX;

        //TODO: Too many if/else statements?

        if (IsGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.States.IdleState);
        }
        else if (IsTouchingWall && !IsTouchingLedge && !IsGrounded)
        {
            StateMachine.ChangeState(Player.States.LedgeClimbState);
        }
        else if (JumpInput && IsTouchingWall)
        {
            IsTouchingWall = Player.CheckIfTouchingWall();
            Player.States.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            StateMachine.ChangeState(Player.States.WallJumpState);
        }
        else if (JumpInput && IsGrounded)
        {
            StateMachine.ChangeState(Player.States.JumpState);
        }
        else if (IsTouchingWall && GrabInput && IsTouchingLedge)
        {
            StateMachine.ChangeState(Player.States.WallGrabState);
        }
        else if (IsTouchingWall && InputX == Player.FacingDirection && Player.CurrentVelocity.y <= 0f)
        {
            StateMachine.ChangeState(Player.States.WallSlideState);
        }
        else
        {
            Player.CheckIfShouldFlip(InputX);
            Player.SetVelocityX(Player.Data.MovementVelocity * InputX);

            Player.Components.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);
        }



        if (IsTouchingWall && !IsTouchingLedge)
        {
            Player.States.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    //TODO: Coyote Time
    //TODO: Jump Variable
}