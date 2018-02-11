using UnityEngine;

[DisallowMultipleComponent]
public class AIBehaviour_Idle : AIBehaviour
{
	
	public override AIBehaviourId BehaviourId { get { return AIBehaviourId.Idle; } }
	public override GoalFlags AchievedGoals { get { return GoalFlags.None; } }
	public override GoalFlags PrerequisiteGoals { get { return GoalFlags.None; } }
	
	// --------------------------------------------------------------------------------

	protected override void OnValidate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnExit()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void Reset()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalAchieved()
	{
		return true;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalInvalid()
	{
		return false;
	}

}
