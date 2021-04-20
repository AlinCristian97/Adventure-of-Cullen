using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player) : base(player)
    {
    }

    public override void Execute()
    {
        base.Execute();

        Player.CheckIfShouldFlip(InputX);

        Player.SetVelocityX(Player.Data.MovementVelocity * InputX);

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