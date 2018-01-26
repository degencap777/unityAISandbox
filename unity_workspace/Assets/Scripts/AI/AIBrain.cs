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

	private Perception_Visual m_visualPerception = null;
	public Perception_Visual Visual { get { return m_visualPerception; } }

	private Perception_Audible m_audiblePerception = null;
	public Perception_Audible Audible { get { return m_audiblePerception; } }

	private List<AIBrainComponent> m_brainComponents = new List<AIBrainComponent>();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_owner = GetComponentInParent<Agent>();
		m_workingMemory = GetComponentInChildren<WorkingMemory>();
		m_behaviourController = GetComponentInChildren<AIBehaviourController>();
		m_visualPerception = GetComponentInChildren<Perception_Visual>();
		m_audiblePerception = GetComponentInChildren<Perception_Audible>();

		m_brainComponents.Add(m_workingMemory);
		m_brainComponents.Add(m_behaviourController);
		m_brainComponents.Add(m_visualPerception);
		m_brainComponents.Add(m_audiblePerception);
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
