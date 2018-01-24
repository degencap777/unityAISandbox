using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
	
	public abstract AIBehaviourId BehaviourId { get; }
	public abstract GoalFlags AchievedGoals { get; }
	public abstract GoalFlags PrerequisiteGoals { get ; }
	
	// --------------------------------------------------------------------------------

	public virtual void OnStart()
	{
		m_owner = owner;
		m_ownerTransform = m_owner != null ? m_owner.transform : null;

		m_workingMemory = workingMemory;
	}

	// --------------------------------------------------------------------------------

	public virtual void OnDestroy()
	{
		OnExit();
	}

	// --------------------------------------------------------------------------------

	protected abstract void OnValidate();
	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();
	public abstract void Reset();

	public abstract bool IsGoalAchieved();
	public abstract bool IsGoalInvalid();
	
}
