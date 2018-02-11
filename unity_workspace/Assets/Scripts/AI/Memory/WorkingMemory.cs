using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WorkingMemory : AIBrainComponent, IAIMemory
{

	[SerializeField]
	private List<Agent> m_initialTargets = new List<Agent>();

	[SerializeField]
	private List<Agent> m_initialAllies = new List<Agent>();

	// --------------------------------------------------------------------------------

	private List<AgentPriority> m_targets = new List<AgentPriority>();
	private List<Agent> m_expiredTargets = new List<Agent>();

	private List<Agent> m_allies = new List<Agent>();
	private List<Agent> m_expiredAllies = new List<Agent>();

	// --------------------------------------------------------------------------------

	public delegate void WorkingMemoryChanged(WorkingMemory sender);
	public event WorkingMemoryChanged OnTargetsChanged;
	public event WorkingMemoryChanged OnAlliesChanged;

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		m_initialTargets.ForEach(target => AddTarget(target));
		m_initialAllies.ForEach(ally => AddAlly(ally));
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}
	
	// --------------------------------------------------------------------------------

	public void AddTarget(Agent target)
	{
		// exit if we already have this target
		for (int i = 0; i < m_targets.Count; ++i)
		{
			if (m_targets[i].Agent == target && 
				m_expiredTargets.Contains(target) == false)
			{
				return;
			}
		}

		AgentPriority agentPriority = AgentPriority.Pool.Get();
		agentPriority.Agent = target;
		agentPriority.Priority = 1.0f;

		m_targets.Add(agentPriority);

		NotifyTargetsChanged();
	}

	// --------------------------------------------------------------------------------

	public void AddAlly(Agent ally)
	{
		// exit if we already have this ally
		if (m_allies.Contains(ally) && 
			m_expiredAllies.Contains(ally) == false)
		{
			return;
		}

		m_allies.Add(ally);

		NotifyAlliesChanged();
	}

	// --------------------------------------------------------------------------------

	public void RemoveTarget(Agent target)
	{
		m_expiredTargets.Add(target);
	}

	// --------------------------------------------------------------------------------

	public void RemoveAlly(Agent ally)
	{
		m_expiredAllies.Add(ally);
	}
	
	// --------------------------------------------------------------------------------

	public void RemoveExpiredTargets()
	{
		bool targetRemoved = false;
		for (int r = 0; r < m_expiredTargets.Count; ++r)
		{
			// find
			int index = -1;
			for (int t = 0; t < m_targets.Count; ++t)
			{
				if (m_targets[t].Agent == m_expiredTargets[r])
				{
					index = t;
					break;
				}
			}

			targetRemoved |= (index > -1);

			// remove and return to pool
			if (index >= 0 && index < m_targets.Count)
			{
				AgentPriority agentPriority = m_targets[index];
				m_targets.RemoveAt(index);
				AgentPriority.Pool.Return(agentPriority);
			}
		}

		// notify subscribers once all requested targets removed
		if (targetRemoved)
		{
			NotifyTargetsChanged();
		}
	}

	// --------------------------------------------------------------------------------

	public void RemoveExpiredAllies()
	{
		bool allyRemoved = false;

		// remove all allies requested
		for (int i = 0; i < m_expiredAllies.Count; ++i)
		{
			allyRemoved |= m_allies.Remove(m_expiredAllies[i]);
		}

		// notify subscribers once all requested allies removed
		if (allyRemoved)
		{
			NotifyAlliesChanged();
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
			m_targets[0].Agent :
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

	// --------------------------------------------------------------------------------

	public void ProcessPerceptionEvent(PerceptionEvent percievedEvent)
	{
		// #SteveD	>>> process event, do we want to add a target/ally?
		//			>>> need to know event actor's allegiance, or event target's allegiance to determine action warranted
		//			>>> what should trigger a change in behaviour?
	}

	// --------------------------------------------------------------------------------
	
#if UNITY_EDITOR

	public List<AgentPriority> Editor_Targets { get { return m_targets; } }
	public List<Agent> Editor_Allies { get { return m_allies; } }

	// --------------------------------------------------------------------------------

	public void Editor_ClearTargets()
	{
		if (m_targets.Count == 0)
		{
			return;
		}

		// return all objects to pool
		for (int i = 0; i < m_targets.Count; ++i)
		{
			AgentPriority.Pool.Return(m_targets[i]);
		}
		// clear targets
		m_targets.Clear();
		// notify subscribers
		NotifyTargetsChanged();
	}

	// --------------------------------------------------------------------------------

	public void Editor_ClearAllies()
	{
		// clear allies
		m_allies.Clear();
		// notify subscribers
		NotifyAlliesChanged();
	}
	
#endif // UNITY_EDITOR

}