using UnityEngine;

public abstract class AIBehaviour : AIBrainComponent
{
	
	public abstract AIBehaviourId BehaviourId { get; }
	public abstract GoalFlags AchievedGoals { get; }
	public abstract GoalFlags PrerequisiteGoals { get ; }
	
	// --------------------------------------------------------------------------------

	public virtual void OnDestroy()
	{
		OnExit();
	}

	// --------------------------------------------------------------------------------

	public abstract void OnStart();
	protected abstract void OnValidate();

	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();
	public abstract void Reset();

	public abstract bool IsGoalAchieved();
	public abstract bool IsGoalInvalid();
	
}
