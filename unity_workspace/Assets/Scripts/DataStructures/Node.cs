

public class Node<T>
{

	private T m_data = default(T);
	public T Data
	{
		get { return m_data; }
		set { m_data = value; }
	}

	private Node<T> m_left = null;
	public Node<T> Left
	{
		get { return m_left; }
		set { m_left = value; }
	}

	private Node<T> m_right = null;
	public Node<T> Right
	{
		get { return m_right; }
		set { m_right = value; }
	}

	// --------------------------------------------------------------------------------

	public Node(T data)
	{
		m_data = data;
	}
	
}