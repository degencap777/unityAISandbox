using UnityEngine;

public class AIBrain : MonoBehaviour
{

	[SerializeField]
	private Agent m_owner = null;

	// #SteveD >>> temporary until AI can percieve Agents
	[SerializeField]
	private Agent m_player = null;
	// -------

	private WorkingMemory m_workingMemory = null;
	private BehaviourCollection m_behaviours = null;

	// --------------------------------------------------------------------------------
	
	protected virtual void Awake()
	{
		m_workingMemory = GetComponentInChildren<WorkingMemory>();
		m_behaviours = GetComponentInChildren<BehaviourCollection>();
	}

	// --------------------------------------------------------------------------------

	protected virtual void Start()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.OnStart();

			// #SteveD >>> temporary until AI can percieve Agents
			if (m_player != null)
			{
				m_workingMemory.AddTarget(m_player);
			}
			// -------
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
