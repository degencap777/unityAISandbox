using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{

	[SerializeField]
	private List<Agent> m_initialTargets = new List<Agent>();

	[SerializeField]
	private List<Agent> m_initialAllies = new List<Agent>();

	// --------------------------------------------------------------------------------

	private Agent m_owner = null;
	public Agent Owner { get { return m_owner; } }

	private WorkingMemory m_workingMemory = null;
	// #SteveD >>> rename this accessor to avoid clash with class name
	public WorkingMemory WorkingMemory { get { return m_workingMemory; } }

	private AIBehaviourCollection m_behaviours = null;
	public AIBehaviourCollection Behaviours { get { return m_behaviours; } }

	private Perception_Visual m_visualPerception = null;
	public Perception_Visual Sight { get { return m_visualPerception; } }

	private Perception_Audible m_audiblePerception = null;
	public Perception_Audible Hearing { get { return m_audiblePerception; } }

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_owner = GetComponentInParent<Agent>();
		m_workingMemory = GetComponentInChildren<WorkingMemory>();
		m_behaviours = GetComponentInChildren<AIBehaviourCollection>();
		m_visualPerception = GetComponentInChildren<Perception_Visual>();
		m_audiblePerception = GetComponentInChildren<Perception_Audible>();
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
			m_behaviours.OnStart();
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
