namespace Player.FSM.States
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public PlayerCrouchIdleState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Player.VelocityModifier.SetVelocityZero();
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
                if (InputX != 0)
                {
                    StateMachine.ChangeState(Player.States.CrouchMoveState);
                }
                else if (InputY != -1 && !IsTouchingCeiling)
                {
                    StateMachine.ChangeState(Player.States.IdleState);
                }
            }
        }
    }
}