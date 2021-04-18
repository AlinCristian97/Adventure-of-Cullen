public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.SetVelocityY(PlayerData.WallClimbVelocity);

            if (InputY != 1)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}