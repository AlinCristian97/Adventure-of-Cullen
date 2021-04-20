using UnityEngine;

public abstract class PlayerAbilityState : PlayerState
{
    protected bool IsAbilityDone;

    protected PlayerAbilityState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsAbilityDone = false;
    }
    
    public override void Execute()
    {
        base.Execute();
        if (IsAbilityDone)
        {
            if (IsGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.States.IdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.States.AirState);
            }
        }
    }
}