public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetColliderHeight(Player.Data.CrouchColliderHeight);

    }

    public override void Exit()
    {
        base.Exit();

        Player.SetColliderHeight(Player.Data.StandColliderHeight);
    }

    public override void Execute()
    {
        base.Execute();

        if (!IsExitingState)
        {
            Player.SetVelocityX(Player.Data.CrouchMovementVelocity * Player.FacingDirection);
            Player.CheckIfShouldFlip(InputX);

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