namespace Player.FSM.States
{
    public class PlayerCrouchMoveState : PlayerGroundedState
    {
        public PlayerCrouchMoveState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Player.ColliderModifier.SetColliderHeight(Player.StatesData._crouchColliderHeight);

        }

        public override void Exit()
        {
            base.Exit();

            Player.ColliderModifier.SetColliderHeight(Player.StatesData._standColliderHeight);
        }

        public override void Execute()
        {
            base.Execute();

            if (!IsExitingState)
            {
                Player.VelocityModifier.SetVelocityX(Player.StatesData._crouchMovementVelocity * Player.Utilities.FacingDirection);
                Player.Utilities.HandleFlip(InputX);

                if (InputX == 0)
                {
                    StateMachine.ChangeState(Player.States.CrouchIdleState);
                }
                else if (InputY != -1 && !IsTouchingCeiling)
                {
                    StateMachine.ChangeState(Player.States.MoveState);
                }
            }
        }
    }
}