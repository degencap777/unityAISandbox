using System.Collections.Generic;

namespace AISandbox.Graph
{
	public class Graph<T>
	{

		protected List<GraphNode<T>> m_nodes = new List<GraphNode<T>>();
		public List<GraphNode<T>>.Enumerator NodeEnumerator { get { return m_nodes.GetEnumerator(); } }

		// --------------------------------------------------------------------------------

		public void Add(GraphNode<T> node)
		{
			if (m_nodes.Contains(node) == false)
			{
				m_nodes.Add(node);
			}
		}

		// --------------------------------------------------------------------------------

		public void Clear()
		{
			m_nodes.Clear();
		}

	}
}