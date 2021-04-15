using FiniteStateMachine;

public class Enemy1IdleState : IdleState
{
    private Enemy1 _enemy;
    
    public Enemy1IdleState(Entity entity, FiniteStateMachine.FiniteStateMachine stateMachine, string animatorBoolName, IdleStateData stateData, Enemy1 enemy) : base(entity, stateMachine, animatorBoolName, stateData)
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

        if (IsIdleTimeOver)
        {
            StateMachine.ChangeState(_enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}