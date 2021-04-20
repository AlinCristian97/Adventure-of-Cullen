using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
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

    public override void Execute()
    {
        base.Execute();

        InputX = Player.InputHandler.NormalizedInputX;

        //TODO: Too many if/else statements?
        
        if (IsGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (IsTouchingWall && !IsTouchingLedge && !IsGrounded)
        {
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
        else if (JumpInput && IsTouchingWall)
        {
            IsTouchingWall = Player.CheckIfTouchingWall();
            Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (JumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (IsTouchingWall && GrabInput && IsTouchingLedge)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (IsTouchingWall && InputX == Player.FacingDirection && Player.CurrentVelocity.y <= 0f)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Player.CheckIfShouldFlip(InputX);
            Player.SetVelocityX(PlayerData.MovementVelocity * InputX);
            
            Player.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);
        }
        
        
        
        if (IsTouchingWall && !IsTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    //TODO: Coyote Time
    //TODO: Jump Variable
}