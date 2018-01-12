using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{

	protected Agent m_owner = null;
	protected WorkingMemory m_workingMemory = null;

	// --------------------------------------------------------------------------------

	public abstract BehaviourId BehaviourId { get; }
	public abstract GoalFlags AchievedGoals { get; }
	public abstract GoalFlags PrerequisiteGoals { get ; }
	
	// --------------------------------------------------------------------------------

	public virtual void Initialise(Agent owner, WorkingMemory workingMemory)
	{
		m_owner = owner;
		m_workingMemory = workingMemory;
	}

	// --------------------------------------------------------------------------------
	
	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();

	public abstract bool IsGoalAchieved();
	public abstract bool IsGoalInvalidated();
	
}
