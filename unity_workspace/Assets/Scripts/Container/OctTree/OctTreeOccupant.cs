using UnityEngine;

namespace AISandbox.Container
{
	[RequireComponent(typeof(SphereCollider))]
	public class OctTreeOccupant : MonoBehaviour
	{

		private SphereCollider m_sphereCollider = null;
		public SphereCollider SphereCollider { get { return m_sphereCollider; } }

		private Transform m_transform = null;
		public Transform Transform { get { return m_transform; } }

		// --------------------------------------------------------------------------------

		protected virtual void Awake()
		{
			m_sphereCollider = GetComponent<SphereCollider>();
			Debug.Assert(m_sphereCollider != null, "[OctTreeOccupant::Awake] GetComponent<SphereCollider> failed");

			m_transform = GetComponent<Transform>();
			Debug.Assert(m_transform != null, "[OctTreeOccupant::Awake] GetComponent<Transform> failed");
		}

	}
}