using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.SetVelocity(PlayerData.WallJumpVelocity, PlayerData.WallJumpAngle, _wallJumpDirection);
        Player.CheckIfShouldFlip(_wallJumpDirection);
        Player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void Execute()
    {
        base.Execute();
        
        Player.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);

        if (Time.time >= StartTime + PlayerData.WallJumpTime)
        {
            IsAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -Player.FacingDirection;
        }
        else
        {
            _wallJumpDirection = Player.FacingDirection;
        }
    }
}