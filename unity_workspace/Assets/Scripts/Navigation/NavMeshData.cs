using System;
using System.Collections.Generic;
using UnityEngine;
using NavMeshGraph = NavMesh.NavMeshGraph;

public class NavMeshData : ScriptableObject
{

	[Serializable]
	public class NavMeshNodeData
	{
		public Vector3 m_position = Vector3.zero;
		public List<int> m_connections = new List<int>();
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

	[SerializeField]
	private List<NavMeshNodeData> m_nodeData = new List<NavMeshNodeData>();

	// --------------------------------------------------------------------------------

	public void SaveGraph(NavMeshGraph graph)
	{
		if (graph == null)
		{
			Debug.LogError("[NavMeshData::SaveData] Failed, graph is null");
			return;
		}

		m_nodeData.Clear();

		var nodeEnumerator = graph.NodeEnumerator;
		while (nodeEnumerator.MoveNext())
		{
			NavMeshNodeData nodeData = new NavMeshNodeData();
			nodeData.m_position = nodeEnumerator.Current.Data;

			var edgeEnumerator = nodeEnumerator.Current.EdgeEnumerator;
			while (edgeEnumerator.MoveNext())
			{
				// #SteveD	>>> how to represent a connected node?
			}

			m_nodeData.Add(nodeData);
		}
	}

	// --------------------------------------------------------------------------------

	public void LoadGraph(out NavMeshGraph graph)
	{
		graph = new NavMeshGraph();
		var nodes = new List<GraphNode<Vector3>>();

		for (int i = 0; i < m_nodeData.Count; ++i)
		{
			nodes.Add(new GraphNode<Vector3>(m_nodeData[i].m_position));
		}
		
		// #SteveD	>>> re-establish connections
	}

}
