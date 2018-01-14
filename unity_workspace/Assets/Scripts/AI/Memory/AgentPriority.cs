using System;

public class AgentPriority : IComparable, IPooledObject
{

	private static ObjectPool<AgentPriority> m_agentPriorityPool = new ObjectPool<AgentPriority>(4, new AgentPriority());
	public static ObjectPool<AgentPriority> Pool { get { return m_agentPriorityPool; } }

	// --------------------------------------------------------------------------------

	private Agent m_target = null;
	public Agent Target 
	{ 
		get { return m_target; } 
		set { m_target = value; }
	}

	// --------------------------------------------------------------------------------

	private float m_priority = 1.0f;
	public float Priority 
	{ 
		get { return m_priority; } 
		set { m_priority = value; }
	}

	// --------------------------------------------------------------------------------

	public void UpdatePriority(float newPriority)
	{
		m_priority = newPriority;
	}

	// --------------------------------------------------------------------------------

	public int CompareTo(object obj)
	{
		AgentPriority otherAgentPriority = obj as AgentPriority;
		if (otherAgentPriority != null)
		{
			return m_priority.CompareTo(otherAgentPriority.m_priority);
		}
		return -1;
	}

	// --------------------------------------------------------------------------------

	public void ReleaseResources()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void Reset()
	{
		m_target = null;
		m_priority = 0.0f;
	}

}
