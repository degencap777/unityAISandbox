using System;
using System.Collections.Generic;

public abstract class Behaviour
{

	protected Agent m_owner = null;
	protected Memory m_memory = null;
	
	// --------------------------------------------------------------------------------

	public abstract Goal AchievesGoal { get; }
	public virtual List<Goal> PrerequisiteGoals { get { return null; } }
	
	// --------------------------------------------------------------------------------

	public Behaviour(Agent owner, Memory memory)
	{
		m_owner = owner;
		m_memory = memory;
	}

	// --------------------------------------------------------------------------------
	
	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();

	public abstract bool IsGoalAchieved();
	public abstract bool IsGoalInvalidated();
	
}
