public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player) : base(player)
    {
    }

    public override void Execute()
    {
        base.Execute();

        if (!IsExitingState)
        {
            Player.SetVelocityY(Player.Data.WallClimbVelocity);

            if (InputY != 1)
            {
                StateMachine.ChangeState(Player.States.WallGrabState);
            }
        }
    }
}