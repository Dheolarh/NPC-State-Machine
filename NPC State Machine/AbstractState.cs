namespace NPC_State_Machine
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}