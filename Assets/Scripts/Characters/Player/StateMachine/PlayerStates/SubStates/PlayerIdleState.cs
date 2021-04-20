using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(0f);
    }
    
    public override void Execute()
    {
        base.Execute();

        if (!IsExitingState)
        {
            if (InputX != 0)
            {
                StateMachine.ChangeState(Player.States.MoveState);
            }
            else if (InputY == -1)
            {
                StateMachine.ChangeState(Player.States.CrouchIdleState);
            }
        }
    }
}