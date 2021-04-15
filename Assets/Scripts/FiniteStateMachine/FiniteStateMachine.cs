namespace FiniteStateMachine
{
    public class FiniteStateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            // CHECK: Can make use of 'ref' / 'out' keywords?
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}