using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{

	private Agent m_owner = null;
	public Agent Owner { get { return m_owner; } }

	private WorkingMemory m_workingMemory = null;
	public WorkingMemory WorkMemory { get { return m_workingMemory; } }

	private AIBehaviourController m_behaviourController = null;
	public AIBehaviourController BehaviourController { get { return m_behaviourController; } }
	
	private List<AIBrainComponent> m_brainComponents = new List<AIBrainComponent>();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_owner = GetComponentInParent<Agent>();
		m_workingMemory = GetComponentInChildren<WorkingMemory>();
		m_behaviourController = GetComponentInChildren<AIBehaviourController>();
		
		// add brain components
		m_brainComponents.Add(m_workingMemory);
		m_brainComponents.Add(m_behaviourController);

		// add all perception brain components
		var perceptions = GetComponentsInChildren<Perception>(true);
		m_brainComponents.AddRange(perceptions);
	}

	// --------------------------------------------------------------------------------

	protected virtual void Start()
	{
		for (int i = 0; i < m_brainComponents.Count; ++i)
		{
			m_brainComponents[i].OnStart();
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		for (int i = 0; i < m_brainComponents.Count; ++i)
		{
			m_brainComponents[i].OnUpdate();
		}
	}

}
