using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Player.SetVelocityX(0f);
        Debug.Log(IsTouchingCeiling);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (InputX != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (InputY == -1)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
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