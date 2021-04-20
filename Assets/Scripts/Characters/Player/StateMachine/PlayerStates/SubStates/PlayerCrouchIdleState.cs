using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityZero();
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