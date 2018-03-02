using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Vision : Perception
{
	
	[SerializeField]
	private List<string> m_raycastLayers = new List<string>();
	private int m_raycastLayerMask = -1;

	// --------------------------------------------------------------------------------

	private Ray m_rayCastRay = new Ray(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f));
	private RaycastHit m_raycastHitInfo;

	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private class RaycastGizmoData : IPooledObject
	{

		private static ObjectPool<RaycastGizmoData> m_pool = 
			new ObjectPool<RaycastGizmoData>(32, new RaycastGizmoData());
		public static ObjectPool<RaycastGizmoData> Pool { get { return m_pool; } }

		// ----------------------------------------------------------------------------

		public Vector3 m_origin = new Vector3(0.0f, 0.0f, 0.0f);
		public Vector3 m_destination = new Vector3(0.0f, 0.0f, 0.0f);
		public float m_lifetimeRemaining = -1.0f;

		// ----------------------------------------------------------------------------

		void IPooledObject.ReleaseResources() { }

		// ----------------------------------------------------------------------------

		void IPooledObject.Reset()
		{
			m_origin.Set(0.0f, 0.0f, 0.0f);
			m_destination.Set(0.0f, 0.0f, 0.0f);
		}

	}

	[SerializeField]
	private float m_raycastGizmoLifetime = 3.0f;

	[SerializeField]
	private Color m_gizmoColour = Color.cyan;
	
	private List<RaycastGizmoData> m_raycastGizmoData = new List<RaycastGizmoData>();
	
#endif // UNITY_EDITOR

	// --------------------------------------------------------------------------------

	public override PerceptionType PerceptionType { get { return PerceptionType.Vision; } }

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		base.OnAwake();

		m_raycastLayerMask = LayerMask.GetMask(m_raycastLayers.ToArray());
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
#if UNITY_EDITOR

		for (int i = 0; i < m_raycastGizmoData.Count; ++i)
		{
			m_raycastGizmoData[i].m_lifetimeRemaining -= Time.deltaTime;
			if (m_raycastGizmoData[i].m_lifetimeRemaining <= 0.0f)
			{
				RaycastGizmoData.Pool.Return(m_raycastGizmoData[i]);
				m_raycastGizmoData[i] = null;
			}
		}
		m_raycastGizmoData.RemoveAll((x) => x == null);

#endif // UNITY_EDITOR
	}

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(PerceptionEvent percievedEvent)
	{
		// validate
		if (percievedEvent == null || m_transform == false)
		{
			return false;
		}

		Vector3 eventPosition = percievedEvent.Actor != null ?
			percievedEvent.Actor.Position : 
			percievedEvent.Location;
		
		// cast a ray toward the event/target
		Vector3 toEvent = eventPosition - m_transform.position;
		m_rayCastRay.origin = m_transform.position;
		m_rayCastRay.direction = toEvent.normalized;

		if (Physics.Raycast(m_rayCastRay, out m_raycastHitInfo, toEvent.magnitude, m_raycastLayerMask))
		{
#if UNITY_EDITOR
			RaycastGizmoData raycastGizmoData = RaycastGizmoData.Pool.Get();
			raycastGizmoData.m_origin = m_transform.position;
			raycastGizmoData.m_destination = m_raycastHitInfo.point;
			raycastGizmoData.m_lifetimeRemaining = m_raycastGizmoLifetime;
			if (raycastGizmoData != null)
			{
				m_raycastGizmoData.Add(raycastGizmoData);
			}
#endif
			return true;
		}
		else
		{
#if UNITY_EDITOR
			RaycastGizmoData raycastGizmoData = RaycastGizmoData.Pool.Get();
			raycastGizmoData.m_origin = m_transform.position;
			raycastGizmoData.m_destination = eventPosition;
			raycastGizmoData.m_lifetimeRemaining = m_raycastGizmoLifetime;
			if (raycastGizmoData != null)
			{
				m_raycastGizmoData.Add(raycastGizmoData);
			}
#endif
			return false;
		}
	}

	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	protected virtual void OnDrawGizmos()
	{
		Color cachedColour = Gizmos.color;
		Gizmos.color = m_gizmoColour;
		
		for (int i = 0; i < m_raycastGizmoData.Count; ++i)
		{
			Gizmos.DrawLine(m_raycastGizmoData[i].m_origin, m_raycastGizmoData[i].m_destination);
		}

		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}
