namespace Player.FSM.States
{
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
                Player.VelocityModifier.SetVelocityY(Player.StatesData._wallSlideVelocity);

                if (GrabInput && InputY == 0)
                {
                    StateMachine.ChangeState(Player.States.WallGrabState);
                }
            }
        }
    }
}