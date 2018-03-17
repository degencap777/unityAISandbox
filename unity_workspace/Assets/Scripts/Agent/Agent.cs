using UnityEngine;

[ExecuteInEditMode]
public class Agent : MonoBehaviour
{

	private Transform m_transform = null;
	public Transform Transform { get { return m_transform; } }

	private ComponentCollection m_componentCollection = null;
	public ComponentCollection ComponentCollection { get { return m_componentCollection; } }

	public Vector3 Position { get { return m_transform.position; } }
	private Vector3 m_previousPosition = Vector3.zero;
	public Vector3 PreviousPosition { get { return m_previousPosition; } }
	
	// --------------------------------------------------------------------------------

	public virtual void Awake()
	{
		m_transform = GetComponent<Transform>();
		Debug.Assert(m_transform != null, "[Agent::Awake] GetComponent<Transform> failed\n");

		m_componentCollection = GetComponentInChildren<ComponentCollection>();
		Debug.Assert(m_componentCollection != null, "[Agent::Awake] GetComponent<ComponentCollection> failed\n");
	}

	// --------------------------------------------------------------------------------

	public virtual void Update()
	{
		m_previousPosition = Position;
		
		if (m_componentCollection != null)
		{
			m_componentCollection.OnUpdate();
		}
	}
	
}
