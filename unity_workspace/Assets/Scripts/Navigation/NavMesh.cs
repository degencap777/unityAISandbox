using AISandbox.Graph;
using AISandbox.Utility;
using System;
using System.Collections.Generic;
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
		private AreaBounds m_bounds = new AreaBounds(Vector3.zero, Vector3.zero);

		[SerializeField]
		private NavMeshData m_dataContainer = null;

		[SerializeField]
		private float m_cellDimension = 1.0f;

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

		private static readonly string k_architectureTag = "Architecture";
		private static readonly float k_nodeGizmoRadius = 0.25f;
		private static readonly Vector3 k_up = Vector3.up;

		// --------------------------------------------------------------------------------

		[SerializeField]
		private Color m_nodeColour = Color.red;
		[SerializeField]
		private Color m_edgeColour = Color.blue;
		[SerializeField]
		private Color m_boundaryPlaneColour = new Color(0.0f, 1.0f, 0.0f, 0.5f);

		// --------------------------------------------------------------------------------

		// worker variables
		private RaycastHit[] m_raycastHits = new RaycastHit[2];
		private int m_architectureLayerMask = -1;

		// --------------------------------------------------------------------------------

		protected virtual void OnDrawGizmosSelected()
		{
			Color cachedColour = Gizmos.color;

			if (m_bounds != null)
			{
				Gizmos.color = m_boundaryPlaneColour;

				Vector3 size = m_bounds.Size;
				Vector3 position = m_bounds.MinBounds + (size * 0.5f);
				size.y = size.y <= 0.01f ? 0.01f : size.y;
				Gizmos.DrawCube(position, size);
			}

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

		public void Editor_GenerateUniformGraph()
		{
			m_graph.Clear();
			m_architectureLayerMask = LayerMask.NameToLayer(k_architectureTag);
			
			int cellsX = (int)(m_bounds.Size.x / m_cellDimension);
			int cellsZ = (int)(m_bounds.Size.z / m_cellDimension);

			float dimensionX = m_bounds.Size.x / cellsX;
			float dimensionZ = m_bounds.Size.z / cellsZ;

			float xStart = m_bounds.MinBounds.x;
			float zStart = m_bounds.MinBounds.z;
			Vector3 position = new Vector3(xStart, m_bounds.MinBounds.y, zStart);

			// generate nodes
			List<List<GraphNode<Vector3>>> nodes = new List<List<GraphNode<Vector3>>>();
			for (int z = 0; z <= cellsZ; ++z)
			{
				nodes.Add(new List<GraphNode<Vector3>>());
				for (int x = 0; x <= cellsX; ++x)
				{
					// #SteveD	>>> only add node if we're not inside a piece of architecture (or too close to one?)
					nodes[z].Add(new GraphNode<Vector3>(new Vector3(position.x, position.y, position.z)));
					position.x += dimensionX;
				}
				position.z += dimensionZ;
				position.x = xStart;
			}

			// generate edges
			for (int z = 0; z < nodes.Count; ++z)
			{
				for (int x = 0; x < nodes[z].Count; ++x)
				{
					bool addNorth = z > 0;
					bool addSouth = z < nodes.Count - 1;
					bool addWest = x > 0;
					bool addEast = x < nodes[z].Count - 1;

					if (addNorth) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z - 1][x]); }
					if (addSouth) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z + 1][x]); }
					if (addWest) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z][x - 1]); }
					if (addEast) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z][x + 1]); }
					if (addNorth && addWest) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z - 1][x - 1]); }
					if (addNorth && addEast) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z - 1][x + 1]); }
					if (addSouth && addWest) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z + 1][x - 1]); }
					if (addSouth && addEast) { Editor_AddConnectionIfAvailable(nodes[z][x], nodes[z + 1][x + 1]); }

					m_graph.Add(nodes[z][x]);
				}
			}
		}

		// --------------------------------------------------------------------------------

		private void Editor_AddConnectionIfAvailable(GraphNode<Vector3> node1, GraphNode<Vector3> node2)
		{
			Vector3 toOther = node2.Data - node1.Data;
			int hitCount = Physics.RaycastNonAlloc(node1.Data, toOther.normalized, m_raycastHits, toOther.magnitude, ~m_architectureLayerMask);
			if (hitCount == 0)
			{
				node1.AddConnection(new NavMeshEdge(node1, node2));
			}
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