using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AIBrain : MonoBehaviour, IDistributedUpdatable
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

		AIBrainManager brainManager = AIBrainManager.Instance as AIBrainManager;
		Debug.Assert(brainManager != null, "[AIBrain::Start] AIBrainManager instance is null");
		if (brainManager != null)
		{
			brainManager.RegisterAIBrain(this);
		}
	}

	// --------------------------------------------------------------------------------

	public virtual void Update()
	{
		for (int i = 0; i < m_brainComponents.Count; ++i)
		{
			m_brainComponents[i].OnUpdate();
		}
	}

	// -------------------------------------------------------------------------------/

	public virtual void DistributedUpdate()
	{
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

	// --------------------------------------------------------------------------------

	protected virtual void OnDestroy()
	{
		AIBrainManager brainManager = AIBrainManager.Instance as AIBrainManager;
		if (brainManager != null)
		{
			brainManager.UnregisterAIBrain(this);
		}
	}

}
