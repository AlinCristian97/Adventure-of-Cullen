using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.SetVelocityZero();
        Player.SetColliderHeight(PlayerData.CrouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        Player.SetColliderHeight(PlayerData.StandColliderHeight);
    }

    public override void Execute()
    {
        base.Execute();

        if (!IsExitingState)
        {
            if (InputX != 0)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
            else if (InputY != -1 && !IsTouchingCeiling)
            {
                Debug.Log("isTouchingCeiling: " + IsTouchingCeiling);
                Debug.Log("cIdle");
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}