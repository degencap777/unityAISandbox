using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class Room : MonoBehaviour
{

	// #SteveD	>>> retain list of all inhabitants

	// #SteveD	>>> collision matrix (collide with Agent only, add layer 'room', or something more general purpose?)

	// #SteveD	>>> custom drawer for readonly lists of inhabiting agent

	private BoxCollider m_collider = null;

	// --------------------------------------------------------------------------------

	public delegate void AgentMigration(Agent agent, Room room);
	public event AgentMigration OnAgentEnter;
	public event AgentMigration OnAgentExit;

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_collider = GetComponent<BoxCollider>();
		Debug.Assert(m_collider != null, "[Room] m_collider (BoxCollider) is null");

#if UNITY_EDITOR

		OnValidate();

#endif // UNITY_EDITOR
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnTriggerEnter(Collider collider)
	{
		Agent agent = collider.GetComponentInParent<Agent>();
		if (agent != null && OnAgentEnter != null)
		{
			OnAgentEnter(agent, this);
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnTriggerExit(Collider collider)
	{
		Agent agent = collider.GetComponentInParent<Agent>();
		if (agent != null && OnAgentExit != null)
		{
			OnAgentExit(agent, this);
		}
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private Vector3 m_gizmoCubeDimensions = Vector3.zero;

	// --------------------------------------------------------------------------------

	protected virtual void OnValidate()
	{
		if (m_collider != null)
		{
			m_gizmoCubeDimensions.Set(m_collider.size.x, 0.01f, m_collider.size.z);
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmosSelected()
	{
		DoDrawGizmos();
	}

	// --------------------------------------------------------------------------------

	public virtual void DoDrawGizmos()
	{
		Color cachedColour = Gizmos.color;

		Color color = Color.green;
		color.a = 0.25f;
		Gizmos.color = color;

		Matrix4x4 cachedMatrix = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.Rotate(transform.rotation);

		Gizmos.DrawCube(transform.position, m_gizmoCubeDimensions);
		
		Gizmos.matrix = cachedMatrix;
		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}
