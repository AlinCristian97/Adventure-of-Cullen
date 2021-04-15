using FiniteStateMachine;

public class Enemy1MoveState : MoveState
{
    private Enemy1 _enemy;
    
    public Enemy1MoveState(Entity entity, FiniteStateMachine.FiniteStateMachine stateMachine, string animatorBoolName, MoveStateData stateData, Enemy1 enemy) : base(entity, stateMachine, animatorBoolName, stateData)
    {
        _enemy = enemy;
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

        if (IsDetectingWall || !IsDetectingLedge)
        {
            _enemy.IdleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}