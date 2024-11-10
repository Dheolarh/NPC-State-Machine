namespace NPC_State_Machine
{
    public class FleeState : IState
    {
        private NPCController npc;
        private float fleeDistance = 15f;
        private float updatePathInterval = 0.5f;
        private float lastPathUpdate;

        public FleeState(NPCController controller)
        {
            npc = controller;
        }

        public void Enter()
        {
            npc.animator.SetBool("isFleeing", true);
        }

        public void Execute()
        {
            if (Time.time - lastPathUpdate >= updatePathInterval)
            {
                Vector3 fleeDirection = npc.transform.position - npc.player.position;
                Vector3 fleePosition = npc.transform.position + fleeDirection.normalized * fleeDistance;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(fleePosition, out hit, fleeDistance, NavMesh.AllAreas))
                {
                    npc.MoveToPosition(hit.position);
                }
                lastPathUpdate = Time.time;
            }

            // Check for transitions
            if (!npc.IsPlayerInRange(fleeDistance) && !npc.HasLowHealth())
            {
                npc.stateMachine.ChangeState(new IdleState(npc));
            }
        }

        public void Exit()
        {
            npc.animator.SetBool("isFleeing", false);
        }
    }
}