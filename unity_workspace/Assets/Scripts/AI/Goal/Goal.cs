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
	
}