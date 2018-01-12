using UnityEngine;

public class Agent : MonoBehaviour
{

	private AgentController m_agentController;
	public AgentController AgentController { get { return m_agentController; } }
	
	// --------------------------------------------------------------------------------

	public virtual void Awake()
	{
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
