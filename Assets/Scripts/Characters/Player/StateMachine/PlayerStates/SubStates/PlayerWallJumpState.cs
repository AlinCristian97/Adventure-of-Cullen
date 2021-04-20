using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _wallJumpDirection;

    public PlayerWallJumpState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        Player.SetVelocity(Player.Data.WallJumpVelocity, Player.Data.WallJumpAngle, _wallJumpDirection);
        Player.CheckIfShouldFlip(_wallJumpDirection);
    }

    public override void Execute()
    {
        base.Execute();

        Player.Components.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);

        if (Time.time >= StartTime + Player.Data.WallJumpTime)
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