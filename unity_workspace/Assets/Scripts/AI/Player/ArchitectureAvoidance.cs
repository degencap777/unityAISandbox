using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArchitectureAvoidance : MonoBehaviour
{

	[SerializeField]
	[Range(0.0f, 3.0f)]
	private float m_feelerReach = 0.5f;

	[SerializeField]
	private List<string> m_includedCollisionLayers = new List<string>();

	// --------------------------------------------------------------------------------
	
	private Agent m_agent = null;
	private AgentController m_agentController = null;
	private CapsuleCollider m_agentCollider = null;

	private int m_raycastLayerMask = -1;
	private RaycastHit[] m_feelerResults = new RaycastHit[2];

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		Transform currentparent = transform.parent;
		while (currentparent != null && m_agent == null)
		{
			m_agent = currentparent.GetComponent<Agent>();
			currentparent = currentparent.parent;
		}
		Debug.Assert(m_agent != null, "Owner Agent is null");

		m_raycastLayerMask = LayerMask.GetMask(m_includedCollisionLayers.ToArray());
	}

	// --------------------------------------------------------------------------------
	
	protected virtual void Start()
	{
		if (m_agent != null)
		{
			m_agentController = m_agent.AgentController;
			m_agentCollider = m_agent.MainCollider;
		}
		Debug.Assert(m_agentController != null, "Owner AgentController is null");
		Debug.Assert(m_agentCollider != null, "Owner MainCollider is null");
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		if (m_agentController != null && m_agentCollider != null)
		{
			Vector3 origin = m_agent.Transform.position;
			origin.y += m_agentCollider.bounds.size.y * 0.5f;
			origin.x += m_agentCollider.bounds.size.x * 0.5f;

			int hits = Physics.RaycastNonAlloc(origin, m_agent.Transform.forward, m_feelerResults, m_feelerReach, m_raycastLayerMask);
			if (hits > 0)
			{
				RotateAwayFrom(m_feelerResults[0]);
				return;
			}

			origin.x -= m_agentCollider.bounds.size.x;
			hits = Physics.RaycastNonAlloc(origin, m_agent.Transform.forward, m_feelerResults, m_feelerReach, m_raycastLayerMask);
			if (hits > 0)
			{
				RotateAwayFrom(m_feelerResults[0]);
			}
		}
	}

	// --------------------------------------------------------------------------------

	private void RotateAwayFrom(RaycastHit hitInfo)
	{
		if (m_agent != null && m_agentController != null)
		{
			//Quaternion currentRotation = m_agent.Transform.rotation;
			//float currentAngle = currentRotation.eulerAngles.y;
			//float desiredAngle = currentAngle;

			// #SteveD >>>	rotate agent away from architecture. hitinfo.normal could be useful?
		}
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private static readonly Color k_feelerColour = new Color(0.5f, 0.5f, 0.9f, 1.0f);
	
	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		if (m_agent == null || m_agentCollider == null)
		{
			return;
		}
	
		Color cachedColour = Gizmos.color;
		Gizmos.color = k_feelerColour;

		Vector3 origin = m_agent.Transform.position;
		origin.y += m_agentCollider.bounds.size.y * 0.5f;
		origin.x += m_agentCollider.bounds.size.x * 0.5f;

		Gizmos.DrawLine(origin, origin + m_agent.Transform.forward * m_feelerReach);
		origin.x -= m_agentCollider.bounds.size.x;
		Gizmos.DrawLine(origin, origin + m_agent.Transform.forward * m_feelerReach);

		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}