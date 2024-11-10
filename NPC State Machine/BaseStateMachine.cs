namespace NPC_State_Machine
{
// Base State Machine class
    public class StateMachine
    {
        private IState currentState;

        public void Initialize(IState startingState)
        {
            currentState = startingState;
            currentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public void Update()
        {
            currentState?.Execute();
        }
    }
}