using UnityEngine;

public class NavMesh : MonoBehaviour
{

	public class NavMeshNode : GraphNode<Vector3> 
	{
		public NavMeshNode(Vector3 position) 
			: base(position) 
		{
			;
		}
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

	public class NavMeshEdge : GraphEdge<Vector3>
	{
		public NavMeshEdge(NavMeshNode node1, NavMeshNode node2)
			: base(node1, node2)
		{
			;
		}

		// ----------------------------------------------------------------------------

		protected override float CalculateCost()
		{
			// #SteveD	>>> squared distance tends towards greed bfs, not true A*
			// #SteveD	>>> distance will result in true best path, but will result in a lot of sqrt calls
			return (m_nodes[0].Data - m_nodes[1].Data).magnitude;
		}
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

	[SerializeField]
	private Vector3 m_minBounds = new Vector3(-1.0f, 0.0f, -1.0f);

	[SerializeField]
	private Vector3 m_maxBounds = new Vector3(1.0f, 0.0f, 1.0f);
	
	// --------------------------------------------------------------------------------

	private Graph<Vector3> m_graph = new Graph<Vector3>();

	// --------------------------------------------------------------------------------

	public NavMeshNode FindNearest(Vector3 position)
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

		return nearest as NavMeshNode;
	}
	
	// --------------------------------------------------------------------------------
	//Editor specific -----------------------------------------------------------------

#if UNITY_EDITOR

	private static readonly Color k_nodeColour = Color.red;
	private static readonly Color k_edgeColour = Color.blue;

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		Color cachedColour = Gizmos.color;

		var nodeEnumerator = m_graph.NodeEnumerator;
		while (nodeEnumerator.MoveNext())
		{
			var node = nodeEnumerator.Current;

			Gizmos.color = k_nodeColour;
			Gizmos.DrawSphere(node.Data, 0.2f);

			Gizmos.color = k_edgeColour;
			var edgeEnumerator = node.EdgeEnumerator;
			while (edgeEnumerator.MoveNext())
			{
				Gizmos.DrawLine(edgeEnumerator.Current.Node1.Data, edgeEnumerator.Current.Node1.Data);
			}
		}

		Gizmos.color = cachedColour;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmosSelected()
	{
		// #SteveD	>>> draw plane
	}

	// --------------------------------------------------------------------------------

	public void Editor_GenerateUniformGraph(float cellWidth)
	{
		// #SteveD	>>> generate grid graph
	}

#endif // UNITY_EDITOR

}