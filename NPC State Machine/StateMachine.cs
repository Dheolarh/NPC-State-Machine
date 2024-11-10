namespace NPC_State_Machine
{
// NPC Controller that uses the State Machine
    public class NPCController : MonoBehaviour
    {
        [Header("NPC Settings")]
        public float moveSpeed = 5f;
        public float detectionRange = 10f;
        public float attackRange = 2f;
        public float patrolRadius = 10f;
        public float health = 100f;
        public Transform player;
    
        [Header("Components")]
        public Animator animator;
        public NavMeshAgent agent;

        private StateMachine stateMachine;
        private Vector3 startPosition;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
        
            // Initialize State Machine with Idle State
            stateMachine = new StateMachine();
            stateMachine.Initialize(new IdleState(this));
        }

        private void Update()
        {
            stateMachine.Update();
        }

        // Helper methods used by states
        public bool IsPlayerInRange(float range)
        {
            if (player == null) return false;
            return Vector3.Distance(transform.position, player.position) <= range;
        }

        public bool HasLowHealth()
        {
            return health <= 30f;
        }

        public void MoveToPosition(Vector3 position)
        {
            agent.SetDestination(position);
            animator.SetBool("isWalking", true);
        }

        public void StopMoving()
        {
            agent.SetDestination(transform.position);
            animator.SetBool("isWalking", false);
        }
    }
}