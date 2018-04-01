using System;
using System.Collections.Generic;
using UnityEngine;

using NavMeshGraph = NavMesh.NavMeshGraph;
using NavMeshNode = GraphNode<UnityEngine.Vector3>;
using NavMeshEdge = NavMesh.NavMeshEdge;

[CreateAssetMenu(fileName = "NavMeshData", menuName = "NavMesh Data", order = 1)]
public class NavMeshData : ScriptableObject
{

	public class NodeToUID
	{
		public NavMeshNode m_node = null;
		public int m_uid = -1;
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

	[Serializable]
	public class NavMeshNodeData
	{
		public Vector3 m_position = Vector3.zero;
		public int m_uid = -1;
		public List<int> m_connections = new List<int>();
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

	[SerializeField]
	private List<NavMeshNodeData> m_nodeData = new List<NavMeshNodeData>();
	public int NodeDataCount { get { return m_nodeData.Count; } }

	// --------------------------------------------------------------------------------

	public void SaveGraph(NavMeshGraph graph)
	{
		if (graph == null)
		{
			Debug.LogError("[NavMeshData::SaveData] Failed, graph is null");
			return;
		}

		var mappedNodes = new List<NodeToUID>();
		int nextId = 0;

		// map graph nodes to an id
		var nodeEnumerator = graph.NodeEnumerator;
		while (nodeEnumerator.MoveNext())
		{
			NodeToUID mappedNode = new NodeToUID()
			{
				m_node = nodeEnumerator.Current,
				m_uid = nextId++,
			};
			mappedNodes.Add(mappedNode);
		}

		// save node position and connections
		m_nodeData.Clear();
		for (int i = 0; i < mappedNodes.Count; ++i)
		{
			var node = mappedNodes[i].m_node;
			// position & uid
			NavMeshNodeData nodeData = new NavMeshNodeData()
			{
				m_position = node.Data,
				m_uid = mappedNodes[i].m_uid,
			};
			
			// connections
			var edgeEnumerator = node.EdgeEnumerator;
			while (edgeEnumerator.MoveNext())
			{
				var connectedNode = edgeEnumerator.Current.GetOther(node);
				nodeData.m_connections.Add(mappedNodes.Find(n => n.m_node == connectedNode).m_uid);
			}

			m_nodeData.Add(nodeData);
		}
	}

	// --------------------------------------------------------------------------------

	public void LoadGraph(out NavMeshGraph graph)
	{
		graph = new NavMeshGraph();

		// map graph nodes to an id
		var mappedNodes = new List<NodeToUID>();
		for (int i = 0; i < m_nodeData.Count; ++i)
		{
			mappedNodes.Add(new NodeToUID()
			{
				m_node = new NavMeshNode(m_nodeData[i].m_position),
				m_uid = m_nodeData[i].m_uid,
			});
		}

		// generate graph
		for (int i = 0; i < mappedNodes.Count; ++i)
		{
			NavMeshNode node = mappedNodes[i].m_node;
			NavMeshNodeData nodeData = m_nodeData.Find(nd => nd.m_uid == mappedNodes[i].m_uid);

			for (int j = 0; j < nodeData.m_connections.Count; ++j)
			{
				var connectedNode = mappedNodes.Find(mn => mn.m_uid == nodeData.m_connections[j]).m_node;
				node.AddConnection(new NavMeshEdge(node, connectedNode));
			}

			graph.Add(node);
		}
	}

}
