

namespace AISandbox.Graph
{
	public abstract class GraphEdge<T>
	{

		protected GraphNode<T>[] m_nodes = new GraphNode<T>[] { null, null };
		public GraphNode<T> Node1 { get { return m_nodes[0]; } }
		public GraphNode<T> Node2 { get { return m_nodes[1]; } }

		// --------------------------------------------------------------------------------

		private float m_cost = 0.0f;
		public float Cost { get { return m_cost; } }

		// --------------------------------------------------------------------------------

		protected abstract float CalculateCost();

		// --------------------------------------------------------------------------------

		public GraphEdge(GraphNode<T> node1, GraphNode<T> node2)
		{
			m_nodes[0] = node1;
			m_nodes[1] = node2;
			RecalculateCost();
		}

		// --------------------------------------------------------------------------------

		public GraphNode<T> GetOther(GraphNode<T> original)
		{
			if (Node1 == original)
			{
				return Node2;
			}
			else if (Node2 == original)
			{
				return Node1;
			}
			return null;
		}

		// --------------------------------------------------------------------------------

		public float RecalculateCost()
		{
			m_cost = CalculateCost();
			return m_cost;
		}

	}
}