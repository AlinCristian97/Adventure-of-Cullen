namespace Player.FSM.States
{
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
                Player.VelocityModifier.SetVelocityY(Player.StatesData._wallClimbVelocity);

                if (InputY != 1)
                {
                    StateMachine.ChangeState(Player.States.WallGrabState);
                }
            }
        }
    }
}