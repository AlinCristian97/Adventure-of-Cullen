using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Player.CheckIfShouldFlip(InputX);
        
        Player.SetVelocityX(PlayerData.MovementVelocity * InputX);

        if (!IsExitingState)
        {
            if (InputX == 0f)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (InputY == -1)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}