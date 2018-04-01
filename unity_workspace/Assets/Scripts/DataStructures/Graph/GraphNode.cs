using System.Collections.Generic;

public class GraphNode<T>
{

	protected T m_data = default(T);
	public T Data { get { return m_data; } }

	protected List<GraphEdge<T>> m_edges= new List<GraphEdge<T>>();
	public List<GraphEdge<T>>.Enumerator EdgeEnumerator { get { return m_edges.GetEnumerator(); } }

	// --------------------------------------------------------------------------------

	public GraphNode(T data)
	{
		m_data = data;
	}

	// --------------------------------------------------------------------------------

	public void AddConnection(GraphEdge<T> edge)
	{
		if (m_edges.Contains(edge) == false)
		{
			m_edges.Add(edge);
		}
	}
	
}