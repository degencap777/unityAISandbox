using UnityEngine;

public abstract class Behaviour : ScriptableObject
{

	protected Goal m_goal = null;

	protected WorkingMemory m_workingMemory = null;

	// --------------------------------------------------------------------------------

	public Behaviour(WorkingMemory workingMemory)
	{
		m_workingMemory = workingMemory;
	}

	// --------------------------------------------------------------------------------

	protected abstract void SetUpGoal();
	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();

	// --------------------------------------------------------------------------------

	public bool IsGoalAchieved()
	{
		return m_goal != null && m_goal.IsAchieved();
	}

	// --------------------------------------------------------------------------------

	public bool IsGoalInvalidated()
	{
		return m_goal == null || m_goal.IsInvalidated();
	}
	
}
