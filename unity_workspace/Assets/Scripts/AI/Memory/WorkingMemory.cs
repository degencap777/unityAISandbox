using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory : MonoBehaviour
{

	private List<AgentPriority> m_targets = new List<AgentPriority>();
	private List<Agent> m_allies = new List<Agent>();

	// --------------------------------------------------------------------------------

	public void OnStart()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void AddTarget(Agent target)
	{
		AgentPriority agentPriority = AgentPriority.Pool.Get();
		agentPriority.Target = target;
		agentPriority.Priority = 1.0f;

		m_targets.Add(agentPriority);
	}

	// --------------------------------------------------------------------------------

	public void AddAlly(Agent ally)
	{
		m_allies.Add(ally);
	}

	// --------------------------------------------------------------------------------

	public void RemoveTarget(Agent target)
	{
		// find
		int index = -1;
		for (int i = 0; i < m_targets.Count; ++i)
		{
			if (m_targets[i].Target == target)
			{
				index = i;
				break;
			}
		}

		// return to pool
		if (index >= 0 && index < m_targets.Count)
		{
			AgentPriority agentPriority = m_targets[index];
			m_targets.RemoveAt(index);
			AgentPriority.Pool.Return(agentPriority);
		}
	}

	// --------------------------------------------------------------------------------

	public void SortTargets()
	{
		m_targets.Sort();
	}

	// --------------------------------------------------------------------------------

	public Agent GetHighestPriorityTarget()
	{
		return m_targets.Count > 0 ?
			m_targets[0].Target :
			null;
	}

}