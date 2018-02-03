using UnityEngine;

public class Agent : MonoBehaviour
{

	private Transform m_transform = null;
	public Transform Transform { get { return m_transform; } }

	private AgentController m_agentController;
	public AgentController AgentController { get { return m_agentController; } }
	
	// --------------------------------------------------------------------------------

	public virtual void Awake()
	{
		m_transform = GetComponent<Transform>();
		Debug.Assert(m_transform != null, "[Agent::Awake] GetComponent<Transform> failed\n");

		m_agentController = GetComponent<AgentController>();
		Debug.Assert(m_agentController != null, "[Agent::Awake] GetComponent<AgentController> failed\n");
	}

	// --------------------------------------------------------------------------------

	public virtual void Update()
	{
		if (m_agentController != null)
		{
			m_agentController.OnUpdate();
		}
	}
	
}
