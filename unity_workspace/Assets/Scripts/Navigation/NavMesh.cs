using AISandbox.Graph;
using System;
using UnityEngine;

namespace AISandbox.Navigation
{
	public class NavMesh : MonoBehaviour
	{

		[Serializable]
		public class NavMeshEdge : GraphEdge<Vector3>
		{
			public NavMeshEdge(GraphNode<Vector3> node1, GraphNode<Vector3> node2)
				: base(node1, node2)
			{
				;
			}

			// ----------------------------------------------------------------------------

			protected override float CalculateCost()
			{
				return (m_nodes[0].Data - m_nodes[1].Data).magnitude;
			}
		}

		// --------------------------------------------------------------------------------
		// --------------------------------------------------------------------------------

		public class NavMeshGraph : Graph<Vector3>
		{
		}

		// --------------------------------------------------------------------------------
		// --------------------------------------------------------------------------------
		
		[SerializeField]
		private NavMeshData m_dataContainer = null;
		
		// --------------------------------------------------------------------------------

		private NavMeshGraph m_graph = new NavMeshGraph();

		// --------------------------------------------------------------------------------

		protected virtual void Awake()
		{
			if (m_dataContainer != null)
			{
				m_dataContainer.LoadGraph(out m_graph);
			}
		}

		// --------------------------------------------------------------------------------

		public GraphNode<Vector3> FindNearest(Vector3 position)
		{
			GraphNode<Vector3> nearest = null;
			float nearestDistanceSquared = float.MaxValue;

			var nodeEnumerator = m_graph.NodeEnumerator;
			while (nodeEnumerator.MoveNext())
			{
				float distanceSquared = (nodeEnumerator.Current.Data - position).sqrMagnitude;
				if (distanceSquared < nearestDistanceSquared)
				{
					nearestDistanceSquared = distanceSquared;
					nearest = nodeEnumerator.Current;
				}
			}

			return nearest;
		}

		// --------------------------------------------------------------------------------
		//Editor specific -----------------------------------------------------------------

#if UNITY_EDITOR

		private static readonly float k_nodeGizmoRadius = 0.25f;
		private static readonly Vector3 k_up = new Vector3(0.0f, 1.0f, 0.0f);
		
		// --------------------------------------------------------------------------------

		[SerializeField]
		private Color m_nodeColour = Color.red;
		
		[SerializeField]
		private Color m_edgeColour = Color.blue;
	
		// --------------------------------------------------------------------------------

		protected virtual void OnDrawGizmos()
		{
			Color cachedColour = Gizmos.color;
			Gizmos.color = m_edgeColour;

			var nodeEnumerator = m_graph.NodeEnumerator;
			while (nodeEnumerator.MoveNext())
			{
				var edgeEnumerator = nodeEnumerator.Current.EdgeEnumerator;
				while (edgeEnumerator.MoveNext())
				{
					Vector3 p1 = edgeEnumerator.Current.Node1.Data;
					Vector3 p2 = edgeEnumerator.Current.Node2.Data;
					Vector3 toNode = (p2 - p1);
					Vector3 offset = (Quaternion.AngleAxis(-90, k_up) * toNode).normalized * k_nodeGizmoRadius * 0.5f;

					Gizmos.DrawLine(p1 + offset, p2 + offset);
				}
			}

			Gizmos.color = m_nodeColour;
			
			nodeEnumerator = m_graph.NodeEnumerator;
			while (nodeEnumerator.MoveNext())
			{
				Gizmos.DrawSphere(nodeEnumerator.Current.Data, k_nodeGizmoRadius);
			}

			Gizmos.color = cachedColour;
		}
		
		// --------------------------------------------------------------------------------

		public void Editor_WriteToAsset()
		{
			if (m_dataContainer == null)
			{
				Debug.LogError("[Editor_SaveToScriptableObject] Failed - m_dataContainer is null");
				return;
			}

			m_dataContainer.SaveGraph(m_graph);
		}

		// --------------------------------------------------------------------------------

		public void Editor_ReadFromAsset()
		{
			if (m_dataContainer == null)
			{
				Debug.LogError("[Editor_LoadFromScriptableObject] Failed - m_dataContainer is null");
				return;
			}

			m_dataContainer.LoadGraph(out m_graph);
		}

#endif // UNITY_EDITOR

	}
}