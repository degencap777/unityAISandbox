using System;
using System.Collections.Generic;

public class BTree<T> where T : IComparable<T>
{

	private BTreeNode<T> m_root = null;
	public BTreeNode<T> Root { get { return m_root; } }

	private List<BTreeNode<T>> m_listNodes = new List<BTreeNode<T>>();
	private bool m_listNodesDirty = false;

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

		m_listNodesDirty = true;

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
			
			if (m_root == current)
			{
				m_root = null;
			}

			Insert(current.Left);
			Insert(current.Right);

			m_listNodesDirty = true;
			return true;
		}

		if (Remove(data, current.Left))
		{
			return true;
		}
		return Remove(data, current.Right);
	}

	// --------------------------------------------------------------------------------

	public void Clear()
	{
		if (m_root != null)
		{
			m_root.Clear();
			m_root = null;
		}
		m_listNodesDirty = true;
	}

	// --------------------------------------------------------------------------------

	public List<BTreeNode<T>>.Enumerator Enumerator(TreeTraversal traversal)
	{
		if (m_listNodesDirty)
		{
			m_listNodes.Clear();
			if (m_root != null)
			{
				m_root.GenerateList(m_listNodes, traversal);
			}
		}

		return m_listNodes.GetEnumerator();
	}

}
