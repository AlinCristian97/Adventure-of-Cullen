using FiniteStateMachine;
using UnityEngine;

public class IdleState : State
{
    protected IdleStateData StateData;

    protected bool FlipAfterIdle;
    protected bool IsIdleTimeOver;

    protected float IdleTime;

    public IdleState(Entity entity, FiniteStateMachine.FiniteStateMachine stateMachine, string animatorBoolName, IdleStateData stateData) : base(entity, stateMachine, animatorBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
        Entity.SetVelocity(0f);
        IsIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (FlipAfterIdle)
        {
            Entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + IdleTime)
        {
            IsIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        FlipAfterIdle = flip;
    }
    
    public void SetRandomIdleTime()
    {
        IdleTime = Random.Range(StateData.MinIdleTime, StateData.MaxIdleTime);
    }
}