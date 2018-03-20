using System.Collections.Generic;

public class Graph<T>
{

	protected List<GraphNode<T>> m_nodes = new List<GraphNode<T>>();
	public List<GraphNode<T>>.Enumerator NodeEnumerator { get { return m_nodes.GetEnumerator(); } }
	
	// --------------------------------------------------------------------------------

	public GraphNode<T> FindNode(T data)
	{
		for (int i = 0; i < m_nodes.Count; ++i)
		{
			if (m_nodes[i].Data.Equals(data))
			{
				return m_nodes[i];
			}
		}
		return null;
	}

	// --------------------------------------------------------------------------------

	public void Sort()
	{
		m_nodes.Sort();
	}

}