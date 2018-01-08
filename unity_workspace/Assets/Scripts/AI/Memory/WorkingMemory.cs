using System.Collections.Generic;

public class WorkingMemory
{

	private Agent m_owner = null;
	public Agent Owner { get { return m_owner; } }

	// --------------------------------------------------------------------------------

	private List<TargetPriority> m_targets = new List<TargetPriority>();
	private List<Agent> m_allies = new List<Agent>();

	// --------------------------------------------------------------------------------

	public WorkingMemory(Agent owner)
	{
		m_owner = owner;
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