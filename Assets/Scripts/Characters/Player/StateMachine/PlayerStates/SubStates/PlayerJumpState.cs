using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        Player.SetVelocityY(Player.Data.JumpVelocity);
        IsAbilityDone = true;
    }
}