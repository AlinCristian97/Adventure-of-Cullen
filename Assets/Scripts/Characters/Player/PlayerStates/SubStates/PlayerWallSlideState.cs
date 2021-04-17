public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Player.SetVelocityY(PlayerData.WallSlideVelocity);

        if (GrabInput && InputY == 0)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }
}