using System;
using System.Collections.Generic;
using UnityEngine;

// #SteveD	
//			>>> custom editor - cell dimension and generate grid on single line
//
//			>>> create ScriptableObject to store nodes & edges
//			>>> save to ScriptableObject
//			>>> load from ScriptableObject
//			>>> method & button to save graph to ScriptableObject manually (save to member specified in Editor, 
//					allows different navmesh data to be saved/loaded)
//			>>> method to load graph
//			>>> test with loading different graphs
//			>>> parse from ScriptableObject on Awake
//			>>> automoate saving?
//			>>> load button
//
//			>>> specify Gizmo colours in editor (one line)
//
//			>>> extract method for adding connections between nodes that also includes a raycast to check for architecture

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
	private Bounds m_levelBounds = null;

	[SerializeField]
	private float m_cellDimension = 1.0f;

	[SerializeField, HideInInspector]
	private NavMeshGraph m_graph = new NavMeshGraph();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		if (m_levelBounds == null)
		{
			m_levelBounds = ScriptableObject.CreateInstance<Bounds>();
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

	private static readonly Color k_nodeColour = Color.red;
	private static readonly Color k_edgeColour = Color.blue;
	private static readonly Color k_boundaryPlaneColour = new Color(0.0f, 1.0f, 0.0f, 0.5f);

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmosSelected()
	{
		Color cachedColour = Gizmos.color;
		
		if (m_levelBounds != null)
		{
			Gizmos.color = k_boundaryPlaneColour;
			
			Vector3 dimension = m_levelBounds.Dimension;
			Vector3 position = m_levelBounds.MinBounds + (dimension * 0.5f);
			dimension.y = dimension.y <= 0.01f ? 0.01f : dimension.y;
			Gizmos.DrawCube(position, dimension);
		}

		Gizmos.color = k_edgeColour;
		var nodeEnumerator = m_graph.NodeEnumerator;
		while (nodeEnumerator.MoveNext())
		{
			var edgeEnumerator = nodeEnumerator.Current.EdgeEnumerator;
			while (edgeEnumerator.MoveNext())
			{
				Gizmos.DrawLine(edgeEnumerator.Current.Node1.Data, edgeEnumerator.Current.Node2.Data);
			}
		}

		Gizmos.color = k_nodeColour;
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

		int cellsX = (int)(m_levelBounds.Dimension.x / m_cellDimension);
		int cellsZ = (int)(m_levelBounds.Dimension.z / m_cellDimension);

		float dimensionX = m_levelBounds.Dimension.x / cellsX;
		float dimensionZ = m_levelBounds.Dimension.z / cellsZ;

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

				if (addNorth) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z - 1][x])); }
				if (addSouth) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z + 1][x])); }
				if (addWest) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z][x - 1])); }
				if (addEast) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z][x + 1])); }
				if (addNorth && addWest) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z - 1][x - 1])); }
				if (addNorth && addEast) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z - 1][x + 1])); }
				if (addSouth && addWest) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z + 1][x - 1])); }
				if (addSouth && addEast) { nodes[z][x].AddConnection(new NavMeshEdge(nodes[z][x], nodes[z + 1][x + 1])); }

				m_graph.Add(nodes[z][x]);
			}
		}
	}

#endif // UNITY_EDITOR

}