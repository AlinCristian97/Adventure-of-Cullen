using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;
    
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
        _amountOfJumpsLeft = PlayerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.SetVelocityY(PlayerData.JumpVelocity);
        IsAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        Player.AirState.SetIsJumping();
    }

    public bool CanJump() => _amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
}