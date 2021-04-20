public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.SetColliderHeight(PlayerData.CrouchColliderHeight);

    }

    public override void Exit()
    {
        base.Exit();
        
        Player.SetColliderHeight(PlayerData.StandColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.SetVelocityX(PlayerData.CrouchMovementVelocity * Player.FacingDirection);
            Player.CheckIfShouldFlip(InputX);
            
            if (InputX == 0)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
            else if (InputY != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
        } 
    }
}