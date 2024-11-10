Unity NPC State Machine System
📝 Overview
This system provides a foundation for creating NPCs with complex, reactive behaviors using a state machine architecture. NPCs can seamlessly transition between different states (like idle, patrol, chase, attack, and flee) based on game conditions and player interactions.

🎯 Features
Modular state machine architecture
Easy to extend with new states
Built-in NavMesh integration for pathfinding
Animator integration for smooth state transitions
Five pre-implemented states:
Idle: Basic waiting behavior
Patrol: Random movement within an area
Chase: Player pursuit
Attack: Combat behavior
Flee: Escape behavior when health is low


🛠️ Installation
Create a new folder in your Unity project's Assets folder named Scripts/NPC
Copy all the state machine scripts into this folder
Ensure you have NavMesh baked in your scene (Window > AI > Navigation)

📋 Requirements
Unity 2020.3 or higher
NavMeshAgent component
Animator component (with appropriate animations)
Baked NavMesh in your scene

🔧 Setup
1. Preparing Your NPC GameObject
2. Create a new empty GameObject
3. Add the following components:
    - Capsule Collider
    - Nav Mesh Agent
    - Animator
    - NPCController script


4. Configure NPCController Inspector
   Required Settings:
- Move Speed: NPC movement speed (default: 5)
- Detection Range: Distance to detect player (default: 10)
- Attack Range: Distance to initiate attack (default: 2)
- Patrol Radius: Area for random patrol (default: 10)
- Health: NPC's initial health (default: 100)
- Player: Reference to player Transform
- Animator: Reference to the Animator component

5. Animation Setup
   Required animator parameters:

Bool:
- isWalking
- isChasing
- isAttacking
- isFleeing

Trigger:
- attack
- 
- 
  💻 Code Structure

  Core Components

`  IState Interface
  public interface IState
  {
  void Enter();    // Called when entering the state
  void Execute();  // Called every frame while in the state
  void Exit();     // Called when exiting the state
  }
  StateMachine Class
  public class StateMachine
  {
  public void Initialize(IState startingState);
  public void ChangeState(IState newState);
  public void Update();
  }`
  NPCController Manages the NPC's behavior and state transitions.
  Available States
  IdleState
  NPC remains stationary
  Transitions to:
  Patrol after time limit
  Chase if player detected
  PatrolState
  Random movement within patrol radius
  Transitions to:
  Chase if player detected
  Flee if health low
  ChaseState
  Pursues player
  Transitions to:
  Attack if within range
  Idle if player lost
  Flee if health low
  AttackState
  Attacks player
  Transitions to:
  Chase if player out of range
  Flee if health low
  FleeState
  Runs away from player
  Transitions to:
  Idle when safe and health recovered
  🎮 Usage Example
  // Example of creating a custom state

```public class CustomState : IState
  {
  private NPCController npc;

  public CustomState(NPCController controller)
  {
  npc = controller;
  }

  public void Enter()
  {
  // Setup code when entering state
  }

  public void Execute()
  {
  // Per-frame logic
  // Check conditions for state transitions
  }

  public void Exit()
  {
  // Cleanup code when exiting state
  }
  }


  🔄 State Transitions
  States automatically transition based on conditions:

Idle → Patrol: Time threshold reached
Idle → Chase: Player detected
Patrol → Chase: Player detected
Chase → Attack: Player in range
Chase → Idle: Player lost
Any State → Flee: Health low
⚡ Performance Considerations
Use appropriate detection ranges to avoid unnecessary checks
Adjust path update intervals in Chase and Flee states
Consider using object pooling for multiple NPCs
Optimize NavMesh settings for your specific game needs
🛠️ Customization
Adding New States
Create new class implementing IState
Add transition logic in Execute()
Implement Enter() and Exit() for setup/cleanup
Add transitions from other states as needed
Modifying Existing States
Adjust timing variables (patrol time, attack cooldown)
Modify transition conditions
Add new behaviors within Execute()
Implement additional animations