

public class AIBehaviour_Idle : AIBehaviour
{
	
	public override AIBehaviourId BehaviourId { get { return AIBehaviourId.Idle; } }
	public override GoalFlags AchievedGoals { get { return GoalFlags.None; } }
	public override GoalFlags PrerequisiteGoals { get { return GoalFlags.None; } }

	// #SteveD >>> implement some idle animations and have this behaviour randomly pick 
	// from them at random intervals (use idle scan)
	// -----------
	// animator reference
	// trigger animation
	// animation controller? (to reset conditions)
	// editor exposed delays between idle animations
	// timer between idle animations
	// -----------

	// --------------------------------------------------------------------------------

	protected override void OnValidate()
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
