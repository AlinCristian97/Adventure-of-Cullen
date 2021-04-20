public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player) : base(player)
    {
    }

    public override void Execute()
    {
        base.Execute();

        if (!IsExitingState)
        {
            Player.SetVelocityY(Player.Data.WallSlideVelocity);

            if (GrabInput && InputY == 0)
            {
                StateMachine.ChangeState(Player.States.WallGrabState);
            }
        }
    }
}