using System;

public class BTree<T> where T : IComparable<T>
{

	// #SteveD	>>> Clear
	// #SteveD	>>> Iterate
	// #SteveD	>>> Balance

	private BTreeNode<T> m_root = null;

	// --------------------------------------------------------------------------------

	public void Insert(T data)
	{
		Insert(new BTreeNode<T>(data, null));
	}

	// --------------------------------------------------------------------------------

	private void Insert(BTreeNode<T> node)
	{
		if (node == null)
		{
			return;
		}

		if (m_root == null)
		{
			m_root = node;
			return;
		}

		m_root.Insert(node);
	}

	// --------------------------------------------------------------------------------

	public bool Remove(T data)
	{
		return Remove(data, m_root);
	}

	// --------------------------------------------------------------------------------

	private bool Remove(T data, BTreeNode<T> current)
	{
		if (current == null)
		{
			return false;
		}

		if (data.CompareTo(current.Data) == 0)
		{
			if (current.Parent != null)
			{
				current.Parent.Detach(current);
			}
			
			Insert(current.Left);
			Insert(current.Right);
			return true;
		}

		if (Remove(data, current.Left))
		{
			return true;
		}
		return Remove(data, current.Right);
	}

}
