namespace NPC_State_Machine
{
    public class IdleState : IState
    {
        private NPCController npc;
        private float idleTimer;
        private float maxIdleTime = 3f;

        public IdleState(NPCController controller)
        {
            npc = controller;
        }

        public void Enter()
        {
            npc.StopMoving();
            idleTimer = 0f;
        }

        public void Execute()
        {
            idleTimer += Time.deltaTime;

            // Check for transitions
            if (npc.IsPlayerInRange(npc.detectionRange))
            {
                npc.stateMachine.ChangeState(new ChaseState(npc));
            }
            else if (idleTimer >= maxIdleTime)
            {
                npc.stateMachine.ChangeState(new PatrolState(npc));
            }
        }

        public void Exit()
        {
            // Clean up any idle state specific things
        }
    }
}