using AISandbox.Graph;
using UnityEngine;

namespace AISandbox.Navigation
{
	[ExecuteInEditMode]
	public class NavMeshEditorNode : MonoBehaviour
	{

		private GraphNode<Vector3> m_associatedNode = null;
		public GraphNode<Vector3> AssociatedNode
		{
			get { return m_associatedNode; }
			set { m_associatedNode = value; }
		}

		// ----------------------------------------------------------------------------

		private Transform m_transform = null;
		private Vector3 m_cachedPosition = Vector3.zero;

		// ----------------------------------------------------------------------------

		protected virtual void Awake()
		{
			m_transform = GetComponent<Transform>();
			Debug.Assert(m_transform != null, "[NavMeshEditorNode::Awake] GetComponent<Transform>() failed");
		}

		// ----------------------------------------------------------------------------

		protected virtual void Update()
		{
			if (m_associatedNode != null &&
				m_transform != null &&
				m_cachedPosition != m_transform.position)
			{
				m_associatedNode.Data = transform.position;
			}
		}
		
		// ----------------------------------------------------------------------------

		protected virtual void OnDestroy()
		{
			;
		}

	}
}
