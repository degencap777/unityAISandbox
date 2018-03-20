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
			return (m_nodes[0].Data - m_nodes[1].Data).sqrMagnitude;
		}
	}

	// --------------------------------------------------------------------------------
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

	// #SteveD	>>> method to create navmesh graph given bounds and granularity

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

#endif // UNITY_EDITOR

}