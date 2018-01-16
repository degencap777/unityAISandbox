using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory : MonoBehaviour
{

	private List<AgentPriority> m_targets = new List<AgentPriority>();
	private List<Agent> m_allies = new List<Agent>();

	// --------------------------------------------------------------------------------

	public delegate void ContractChanged();
	public event ContractChanged OnTargetsChanged;
	public event ContractChanged OnAlliesChanged;

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
		TargetsChanged();
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
		AlliesChanged();
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

			TargetsChanged();
		}
	}

	// --------------------------------------------------------------------------------

	public void RemoveAlly(Agent ally)
	{
		m_allies.Remove(ally);
		AlliesChanged();
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

	public void ClearTargets()
	{
		m_targets.Clear();
		TargetsChanged();
	}

	// --------------------------------------------------------------------------------

	public void ClearAllies()
	{
		m_allies.Clear();
		AlliesChanged();
	}

	// --------------------------------------------------------------------------------

	private void TargetsChanged()
	{
		if (OnTargetsChanged != null)
		{
			OnTargetsChanged();
		}
	}

	// --------------------------------------------------------------------------------

	private void AlliesChanged()
	{
		if (OnAlliesChanged != null)
		{
			OnAlliesChanged();
		}
	}

}