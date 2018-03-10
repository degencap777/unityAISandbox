using UnityEngine;

[ExecuteInEditMode]
public class ArchitectureAvoidance : MonoBehaviour
{

	[SerializeField]
	[Range(0.0f, 90.0f)]
	private float m_feelerAngle = 35.0f;
	
	[SerializeField]
	[Range(0.0f, 3.0f)]
	private float m_feelerReach = 0.5f;

	// --------------------------------------------------------------------------------

	private Agent m_agent = null;
	private AgentController m_agentController = null;

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
	}

	// --------------------------------------------------------------------------------
	
	protected virtual void Start()
	{
		if (m_agent != null)
		{
			m_agentController = m_agent.AgentController;
		}
		Debug.Assert(m_agentController != null, "Owner AgentController is null");
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		if (m_agentController != null)
		{
			Vector3 reach = (m_agent.Transform.forward * m_feelerReach);

			Vector3 ray = Quaternion.AngleAxis(m_feelerAngle, Vector3.up) * reach;
			int hits = Physics.RaycastNonAlloc(m_agent.Transform.position, ray, m_feelerResults, m_feelerReach);
			if (hits > 0)
			{
				//this.LogInfo(string.Format("felt {0}", m_feelerResults[0].transform.name));
				RotateAwayFrom(m_feelerResults[0]);
				return;
			}
			
			ray = Quaternion.AngleAxis(-m_feelerAngle, Vector3.up) * reach;
			hits = Physics.RaycastNonAlloc(m_agent.Transform.position, ray, m_feelerResults, m_feelerReach);
			if (hits > 0)
			{
				//this.LogInfo(string.Format("felt {0}", m_feelerResults[0].transform.name));
				RotateAwayFrom(m_feelerResults[0]);
			}
		}
	}

	// --------------------------------------------------------------------------------

	private void RotateAwayFrom(RaycastHit hitInfo)
	{
		if (m_agent != null && m_agentController != null)
		{
			Quaternion currentRotation = m_agent.Transform.rotation;
			float currentAngle = currentRotation.eulerAngles.y;
			float desiredAngle = currentAngle;

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
		if (m_agent == null)
		{
			return;
		}
	
		Color cachedColour = Gizmos.color;
		Gizmos.color = k_feelerColour;

		Vector3 reach = (m_agent.transform.forward * m_feelerReach);
		Vector3 origin = m_agent.transform.position;
		origin.y += 1.0f;

		Vector3 ray = Quaternion.AngleAxis(m_feelerAngle, Vector3.up) * reach;
		Gizmos.DrawLine(origin, origin + ray);
		Gizmos.DrawSphere(origin + ray, 0.1f);

		ray = Quaternion.AngleAxis(-m_feelerAngle, Vector3.up) * reach;
		Gizmos.DrawLine(origin, origin + ray);
		Gizmos.DrawSphere(origin + ray, 0.1f);

		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}