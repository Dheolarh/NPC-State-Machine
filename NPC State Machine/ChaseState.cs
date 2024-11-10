namespace NPC_State_Machine
{
    public class ChaseState : IState
    {
        private NPCController npc;
        private float updatePathInterval = 0.2f;
        private float lastPathUpdate;

        public ChaseState(NPCController controller)
        {
            npc = controller;
        }

        public void Enter()
        {
            npc.animator.SetBool("isChasing", true);
        }

        public void Execute()
        {
            if (Time.time - lastPathUpdate >= updatePathInterval)
            {
                // Update path to player
                npc.MoveToPosition(npc.player.position);
                lastPathUpdate = Time.time;
            }

            // Check for transitions
            if (!npc.IsPlayerInRange(npc.detectionRange))
            {
                npc.stateMachine.ChangeState(new IdleState(npc));
            }
            else if (npc.IsPlayerInRange(npc.attackRange))
            {
                npc.stateMachine.ChangeState(new AttackState(npc));
            }
            else if (npc.HasLowHealth())
            {
                npc.stateMachine.ChangeState(new FleeState(npc));
            }
        }

        public void Exit()
        {
            npc.animator.SetBool("isChasing", false);
        }
    }
}