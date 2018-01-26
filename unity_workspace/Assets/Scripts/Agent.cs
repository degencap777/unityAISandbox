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
		m_agentController = GetComponent<AgentController>();
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
