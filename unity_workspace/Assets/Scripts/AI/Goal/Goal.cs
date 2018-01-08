using UnityEngine;

public abstract class Goal : ScriptableObject
{

	protected WorkingMemory m_workingMemory = null;
	public WorkingMemory WorkingMemory 
	{ 
		get { return m_workingMemory; } 
		set { m_workingMemory = value; }
	}

	// --------------------------------------------------------------------------------

	public abstract bool IsAchieved();
	public abstract bool IsInvalidated();

	// --------------------------------------------------------------------------------

	protected Agent GetOwner()
	{
		return m_workingMemory != null ?
			m_workingMemory.Owner :
			null;
	}

	// --------------------------------------------------------------------------------

	protected Agent GetHighestPriorityTarget()
	{
		return m_workingMemory != null ?
			m_workingMemory.GetHighestPriorityTarget() :
			null;
	}

}