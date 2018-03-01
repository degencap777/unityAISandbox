using UnityEngine;

public class Agent : MonoBehaviour
{

	private Transform m_transform = null;
	public Transform Transform { get { return m_transform; } }

	private AgentController m_agentController;
	public AgentController AgentController { get { return m_agentController; } }

	public Vector3 Position { get { return m_transform != null ? m_transform.position : Vector3.zero; } }
	
	private Vector3 m_previousPosition = Vector3.zero;
	public Vector3 PreviousPosition { get { return m_previousPosition; } }
	
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
		m_previousPosition = Position;

		if (m_agentController != null)
		{
			m_agentController.OnUpdate();
		}
	}
	
}
