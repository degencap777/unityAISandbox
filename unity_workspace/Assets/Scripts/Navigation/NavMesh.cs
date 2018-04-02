using AISandbox.Graph;
using AISandbox.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

// #SteveD	>>> raycasts in Editor_AddConnectionIfAvailable
//			>>> NavMeshData custom property drawer (information only)

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
		private AreaBounds m_levelBounds = null;

		[SerializeField]
		private NavMeshData m_dataContainer = null;

		[SerializeField]
		private float m_cellDimension = 1.0f;

		// --------------------------------------------------------------------------------

		private NavMeshGraph m_graph = new NavMeshGraph();

		// --------------------------------------------------------------------------------

		protected virtual void Awake()
		{
			if (m_levelBounds == null)
			{
				m_levelBounds = ScriptableObject.CreateInstance<AreaBounds>();
			}

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

		[SerializeField]
		private Color m_nodeColour = Color.red;
		[SerializeField]
		private Color m_edgeColour = Color.blue;
		[SerializeField]
		private Color m_boundaryPlaneColour = new Color(0.0f, 1.0f, 0.0f, 0.5f);

		// --------------------------------------------------------------------------------

		protected virtual void OnDrawGizmosSelected()
		{
			Color cachedColour = Gizmos.color;

			if (m_levelBounds != null)
			{
				Gizmos.color = m_boundaryPlaneColour;

				Vector3 dimension = m_levelBounds.Size;
				Vector3 position = m_levelBounds.MinBounds + (dimension * 0.5f);
				dimension.y = dimension.y <= 0.01f ? 0.01f : dimension.y;
				Gizmos.DrawCube(position, dimension);
			}

			Gizmos.color = m_edgeColour;
			var nodeEnumerator = m_graph.NodeEnumerator;
			while (nodeEnumerator.MoveNext())
			{
				var edgeEnumerator = nodeEnumerator.Current.EdgeEnumerator;
				while (edgeEnumerator.MoveNext())
				{
					Gizmos.DrawLine(edgeEnumerator.Current.Node1.Data, edgeEnumerator.Current.Node2.Data);
				}
			}

			Gizmos.color = m_nodeColour;
			nodeEnumerator = m_graph.NodeEnumerator;
			while (nodeEnumerator.MoveNext())
			{
				Gizmos.DrawSphere(nodeEnumerator.Current.Data, 0.2f);
			}

			Gizmos.color = cachedColour;
		}

		// --------------------------------------------------------------------------------

		public void Editor_GenerateUniformGraph()
		{
			m_graph.Clear();

			int cellsX = (int)(m_levelBounds.Size.x / m_cellDimension);
			int cellsZ = (int)(m_levelBounds.Size.z / m_cellDimension);

			float dimensionX = m_levelBounds.Size.x / cellsX;
			float dimensionZ = m_levelBounds.Size.z / cellsZ;

			float xStart = m_levelBounds.MinBounds.x + (dimensionX * 0.5f);
			float zStart = m_levelBounds.MinBounds.z + (dimensionZ * 0.5f);
			Vector3 position = new Vector3(xStart, m_levelBounds.MinBounds.y, zStart);

			// generate nodes
			List<List<GraphNode<Vector3>>> nodes = new List<List<GraphNode<Vector3>>>();
			for (int z = 0; z < cellsZ; ++z)
			{
				nodes.Add(new List<GraphNode<Vector3>>());
				for (int x = 0; x < cellsX; ++x)
				{
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
			// #SteveD	>>> multiple raycasts between nodes to check for obstacles, offset from centre (could use 0.25f * m_cellDimension?)
			node1.AddConnection(new NavMeshEdge(node1, node2));
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