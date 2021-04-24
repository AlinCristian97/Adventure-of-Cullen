namespace Player.FSM.States
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player) : base(player)
        {
        }

        public override void Execute()
        {
            base.Execute();

            Player.Utilities.HandleFlip(InputX);
        
            Player.VelocityModifier.SetVelocityX(Player.StatesData._movementVelocity * InputX);

            if (!IsExitingState)
            {
                if (InputX == 0f)
                {
                    StateMachine.ChangeState(Player.States.IdleState);
                }
                else if (InputY == -1)
                {
                    StateMachine.ChangeState(Player.States.CrouchMoveState);
                }
            }
        }
    }
}