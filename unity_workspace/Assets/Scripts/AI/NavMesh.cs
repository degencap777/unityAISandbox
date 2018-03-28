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
	private LevelBounds m_levelBounds = null;

	// --------------------------------------------------------------------------------

	private Graph<Vector3> m_graph = new Graph<Vector3>();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		if (m_levelBounds == null)
		{
			m_levelBounds = ScriptableObject.CreateInstance<LevelBounds>();
		}
	}

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
	private static readonly Color k_boundaryPlaneColour = new Color(0.0f, 1.0f, 0.0f, 0.5f);

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmosSelected()
	{
		Color cachedColour = Gizmos.color;
		
		if (m_levelBounds != null)
		{
			Gizmos.color = k_boundaryPlaneColour;
			Matrix4x4 cachedMatrix = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.Rotate(transform.rotation);

			Vector3 dimension = m_levelBounds.Dimension;
			Vector3 position = m_levelBounds.MinBounds + (dimension * 0.5f);
			dimension.y = dimension.y <= 0.01f ? 0.01f : dimension.y;
			Gizmos.DrawCube(position, dimension);

			Gizmos.matrix = cachedMatrix;
		}
			
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

	public void Editor_GenerateUniformGraph(float cellWidth)
	{
		// #SteveD	>>> generate grid graph
	}

#endif // UNITY_EDITOR

}