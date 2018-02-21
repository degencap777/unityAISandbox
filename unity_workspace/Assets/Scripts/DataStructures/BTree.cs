using System;
using System.Collections.Generic;
using UnityEngine;

public class BTree<T> where T : IComparable<T>
{

	private BTreeNode<T> m_root = null;
	public BTreeNode<T> Root { get { return m_root; } }

	private List<T> m_nodes = new List<T>();
	private bool m_nodesDirty = false;

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

		m_nodesDirty = true;

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

			m_nodesDirty = true;
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
		m_nodesDirty = true;
	}

	// --------------------------------------------------------------------------------

	public List<T> ToList(TreeTraversal traversal)
	{
		if (m_nodesDirty)
		{
			m_nodes.Clear();
			if (m_root != null)
			{
				m_root.GenerateList(m_nodes, traversal);
			}
		}

		return m_nodes;
	}

	// --------------------------------------------------------------------------------

	public void Log(TreeTraversal traversal)
	{
		Debug.LogFormat("[BTree] {0}\n{1}\n", traversal.ToString(), string.Join("\n", ToList(traversal).ConvertAll<string>(n => n.ToString()).ToArray()));
	}

}
