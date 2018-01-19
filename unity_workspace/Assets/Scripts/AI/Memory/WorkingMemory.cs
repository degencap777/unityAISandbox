using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory : MonoBehaviour
{

	private List<AgentPriority> m_targets = new List<AgentPriority>();
	private List<Agent> m_allies = new List<Agent>();

	// --------------------------------------------------------------------------------

	public delegate void WorkingMemoryChanged(WorkingMemory sender);
	public event WorkingMemoryChanged OnTargetsChanged;
	public event WorkingMemoryChanged OnAlliesChanged;

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
		// exit if we already have this target
		for (int i = 0; i < m_targets.Count; ++i)
		{
			if (m_targets[i].Target == target)
			{
				return;
			}
		}

		AgentPriority agentPriority = AgentPriority.Pool.Get();
		agentPriority.Target = target;
		agentPriority.Priority = 1.0f;

		m_targets.Add(agentPriority);

		NotifyTargetsChanged();
	}

	// --------------------------------------------------------------------------------

	public void AddAlly(Agent ally)
	{
		// exit if we already have this ally
		if (m_allies.Contains(ally))
		{
			return;
		}

		m_allies.Add(ally);

		NotifyAlliesChanged();
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

			NotifyTargetsChanged();
		}
	}

	// --------------------------------------------------------------------------------

	public void RemoveAlly(Agent ally)
	{
		m_allies.Remove(ally);

		NotifyAlliesChanged();
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
	
	// --------------------------------------------------------------------------------

	private void NotifyTargetsChanged()
	{
		if (OnTargetsChanged != null)
		{
			OnTargetsChanged(this);
		}
	}

	// --------------------------------------------------------------------------------

	private void NotifyAlliesChanged()
	{
		if (OnAlliesChanged != null)
		{
			OnAlliesChanged(this);
		}
	}

}