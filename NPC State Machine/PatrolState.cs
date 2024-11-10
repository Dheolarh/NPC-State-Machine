namespace NPC_State_Machine
{
    public class PatrolState : IState
    {
        private NPCController npc;
        private Vector3 currentPatrolPoint;
        private float patrolPointReachedThreshold = 1f;

        public PatrolState(NPCController controller)
        {
            npc = controller;
        }

        public void Enter()
        {
            SetNewPatrolPoint();
        }

        public void Execute()
        {
            // Check if we reached the patrol point
            if (Vector3.Distance(npc.transform.position, currentPatrolPoint) <= patrolPointReachedThreshold)
            {
                SetNewPatrolPoint();
            }

            // Check for transitions
            if (npc.IsPlayerInRange(npc.detectionRange))
            {
                npc.stateMachine.ChangeState(new ChaseState(npc));
            }
            else if (npc.HasLowHealth())
            {
                npc.stateMachine.ChangeState(new FleeState(npc));
            }
        }

        public void Exit()
        {
            npc.StopMoving();
        }

        private void SetNewPatrolPoint()
        {
            // Generate random point within patrol radius
            Vector2 randomCircle = Random.insideUnitCircle * npc.patrolRadius;
            Vector3 randomPoint = npc.startPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
        
            // Ensure point is on NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, npc.patrolRadius, NavMesh.AllAreas))
            {
                currentPatrolPoint = hit.position;
                npc.MoveToPosition(currentPatrolPoint);
            }
        }
    }
}