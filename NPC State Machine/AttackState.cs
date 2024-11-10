namespace NPC_State_Machine
{
  public class AttackState : IState
  {
    private NPCController npc;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    public AttackState(NPCController controller)
    {
      npc = controller;
    }

    public void Enter()
    {
      npc.StopMoving();
      npc.animator.SetBool("isAttacking", true);
      lastAttackTime = -attackCooldown; // Allow immediate first attack
    }

    public void Execute()
    {
      // Face the player
      if (npc.player != null)
      {
        Vector3 direction = (npc.player.position - npc.transform.position).normalized;
        npc.transform.rotation = Quaternion.LookRotation(direction);

        // Attack logic
        if (Time.time - lastAttackTime >= attackCooldown)
        {
          PerformAttack();
          lastAttackTime = Time.time;
        }
      }

      // Check for transitions
      if (!npc.IsPlayerInRange(npc.attackRange))
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
      npc.animator.SetBool("isAttacking", false);
    }

    private void PerformAttack()
    {
      npc.animator.SetTrigger("attack");
      // Add actual attack logic here (e.g., damage dealing)
    }
  }
}