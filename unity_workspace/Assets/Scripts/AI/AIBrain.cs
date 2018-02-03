using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AIBrain : MonoBehaviour
{

	private Agent m_owner = null;
	public Agent Owner { get { return m_owner; } }

	private WorkingMemory m_workingMemory = null;
	public WorkingMemory WorkingMemory { get { return m_workingMemory; } }

	private HistoricMemory m_historicMemory = null;
	public HistoricMemory HistoricMemory { get { return m_historicMemory; } }

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

		// #SteveD >>> only want to call CleanMemories periodically, on a few agents at a time. 
		// requires a utility class somewhere (with which every brain will register itself) - AIBrainManager / AIBrainUtility
		// that has a list of lists of agents that are processed in order (one sublist per update).
		// number of lists = [member variable: seconds between CleanMemories] / (0.01666.. [1.0f / 60.0f])
		// once we reach this number of lists, insert into list 0 again.
		//
		// must keep track of:
		// 1. next list to insert into
		// 2. next list to update
		
		CleanMemories();
	}

	// --------------------------------------------------------------------------------

	public void CleanMemories()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.RemoveExpiredTargets();
			m_workingMemory.RemoveExpiredAllies();
		}

		if (m_historicMemory != null)
		{
			m_historicMemory.RemoveExpiredMemories();
		}
	}

}
