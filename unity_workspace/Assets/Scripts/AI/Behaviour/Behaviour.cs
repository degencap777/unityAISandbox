using UnityEngine;

public abstract class Behaviour : ScriptableObject
{

	protected IGoal m_goal = null;

	// --------------------------------------------------------------------------------

	protected Agent m_owner = null;

	// --------------------------------------------------------------------------------

	public Behaviour(Agent owner)
	{
		m_owner = owner;
	}

	// --------------------------------------------------------------------------------

	public abstract void SetGoal();
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
		return m_goal != null && m_goal.IsInvalidated();
	}
	
}
