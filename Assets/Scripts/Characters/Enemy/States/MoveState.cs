using FiniteStateMachine;

public class MoveState : State
{
    protected MoveStateData StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;
    
    public MoveState(Entity entity, FiniteStateMachine.FiniteStateMachine stateMachine, string animatorBoolName, MoveStateData stateData) : base(entity, stateMachine, animatorBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.SetVelocity(StateData.MovementSpeed);

        IsDetectingLedge = Entity.CheckLedge();
        IsDetectingWall = Entity.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        IsDetectingLedge = Entity.CheckLedge();
        IsDetectingWall = Entity.CheckWall();
    }
}