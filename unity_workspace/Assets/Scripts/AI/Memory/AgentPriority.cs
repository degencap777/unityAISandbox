using System;
using UnityEngine;

[Serializable]
public class AgentPriority : IComparable, IPooledObject
{

	private static ObjectPool<AgentPriority> m_agentPriorityPool = new ObjectPool<AgentPriority>(4, new AgentPriority());
	public static ObjectPool<AgentPriority> Pool { get { return m_agentPriorityPool; } }

	// --------------------------------------------------------------------------------

	[SerializeField, HideInInspector]
	private Agent m_agent = null;
	public Agent Agent 
	{ 
		get { return m_agent; } 
		set { m_agent = value; }
	}

	// --------------------------------------------------------------------------------

	[SerializeField, HideInInspector]
	private float m_priority = 1.0f;
	public float Priority 
	{ 
		get { return m_priority; } 
		set { m_priority = value; }
	}

	// --------------------------------------------------------------------------------

	public void UpdatePriority(float newPriority)
	{
		m_priority = Mathf.Clamp(newPriority, 0.0f, 1.0f);
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
		m_agent = null;
		UpdatePriority(0.0f);
	}

}
