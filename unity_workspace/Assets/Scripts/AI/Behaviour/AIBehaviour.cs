using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{

	protected Agent m_owner = null;
	protected Transform m_ownerTransform = null;

	protected WorkingMemory m_workingMemory = null;

	// --------------------------------------------------------------------------------

	public abstract AIBehaviourId BehaviourId { get; }
	public abstract GoalFlags AchievedGoals { get; }
	public abstract GoalFlags PrerequisiteGoals { get ; }
	
	// --------------------------------------------------------------------------------

	public virtual void OnStart(Agent owner, WorkingMemory workingMemory)
	{
		m_owner = owner;
		m_ownerTransform = m_owner != null ? m_owner.transform : null;

		m_workingMemory = workingMemory;
	}

	// --------------------------------------------------------------------------------

	protected abstract void OnValidate();
	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();

	public abstract bool IsGoalAchieved();
	public abstract bool IsGoalInvalid();
	
}
