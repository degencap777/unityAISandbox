using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{

	[SerializeField]
	private Agent m_owner = null;

	[SerializeField]
	private List<Agent> m_initialTargets = new List<Agent>();

	[SerializeField]
	private List<Agent> m_initialAllies = new List<Agent>();

	// --------------------------------------------------------------------------------

	private WorkingMemory m_workingMemory = null;
	private AIBehaviourCollection m_behaviours = null;

	// --------------------------------------------------------------------------------
	
	protected virtual void Awake()
	{
		m_workingMemory = GetComponentInChildren<WorkingMemory>();
		m_behaviours = GetComponentInChildren<AIBehaviourCollection>();
	}

	// --------------------------------------------------------------------------------

	protected virtual void Start()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.OnStart();
			
			for (int i = 0; i < m_initialTargets.Count; ++i)
			{
				m_workingMemory.AddTarget(m_initialTargets[i]);
			}

			for (int i = 0; i < m_initialAllies.Count; ++i)
			{
				m_workingMemory.AddAlly(m_initialAllies[i]);
			}
		}

		if (m_behaviours != null)
		{
			m_behaviours.OnStart(m_owner, m_workingMemory);
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.OnUpdate();
		}

		if (m_behaviours != null)
		{
			m_behaviours.OnUpdate();
		}
	}

}
