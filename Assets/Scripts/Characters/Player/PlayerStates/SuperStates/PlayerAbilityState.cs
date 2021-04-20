using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool IsAbilityDone;
    
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Execute()
    {
        base.Execute();
        if (IsAbilityDone)
        {
            if (IsGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.AirState);
            }
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
        
    }
}
