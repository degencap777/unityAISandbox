using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory : MonoBehaviour
{

	private List<TargetPriority> m_targets = new List<TargetPriority>();
	private List<Agent> m_allies = new List<Agent>();

	// --------------------------------------------------------------------------------

	public void Initialise()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void OnUpdate()
	{
		;
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