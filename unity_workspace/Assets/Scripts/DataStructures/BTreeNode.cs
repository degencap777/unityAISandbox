using System;
using System.Collections.Generic;

public class BTreeNode<T> where T : IComparable<T>
{

	private T m_data = default(T);
	public T Data { get { return m_data; } }

	private BTreeNode<T> m_left = null;
	public BTreeNode<T> Left { get { return m_left; } }

	private BTreeNode<T> m_right = null;
	public BTreeNode<T> Right { get { return m_right; } }

	private BTreeNode<T> m_parent = null;
	public BTreeNode<T> Parent { get { return m_parent; } }

	public BTreeNode<T> GrandParent
	{
		get { return m_parent != null ? m_parent.m_parent : null; }
	}

	public BTreeNode<T> Sibling 
	{ 
		get 
		{ 
			if (m_parent == null)
			{
				return null;
			}
			return m_parent.Left == this ? m_parent.Right : m_parent.Left;
		}
	}

	public BTreeNode<T> Uncle
	{
		get
		{
			BTreeNode<T> grandParent = GrandParent;
			if (grandParent == null)
			{
				return null;
			}
			return grandParent.Left == m_parent ? grandParent.Right : grandParent.Left;
		}
	}

	public bool IsLeaf { get { return m_left == null && m_right == null; } }

	private Queue<BTreeNode<T>> m_bfsQueue = new Queue<BTreeNode<T>>();

	// --------------------------------------------------------------------------------

	public BTreeNode(T data, BTreeNode<T> parent)
	{
		m_data = data;
		m_parent = parent;
	}

	// --------------------------------------------------------------------------------

	public void Insert(BTreeNode<T> node)
	{
		if (node == null)
		{
			return;
		}

		if (node.Data.CompareTo(m_data) < 0)
		{
			if (m_left != null)
			{
				m_left.Insert(node);
			}
			else
			{
				node.m_parent = this;
				m_left = node;
			}
		}
		else
		{
			if (m_right != null)
			{
				m_right.Insert(node);
			}
			else
			{
				node.m_parent = this;
				m_right = node; 
			}
		}
	}

	// --------------------------------------------------------------------------------

	public void Detach(BTreeNode<T> child)
	{
		if (m_left == child)
		{
			m_left = null;
		}
		else if (m_right == child)
		{
			m_right = null;
		}
	}

	// --------------------------------------------------------------------------------

	public void GenerateList(List<BTreeNode<T>> list, TreeTraversal traversal)
	{
		switch (traversal)
		{
			case TreeTraversal.BreadthFirst:
				m_bfsQueue.Clear();
				m_bfsQueue.Enqueue(this);

				while (m_bfsQueue.Count > 0)
				{
					var next = m_bfsQueue.Dequeue();
					list.Add(next);

					if (next.Left != null)
					{
						m_bfsQueue.Enqueue(next.Left);
					}
					if (next.Right != null)
					{
						m_bfsQueue.Enqueue(next.Right);
					}
				}

				break;

			case TreeTraversal.DepthFirst:
				if (m_left != null)
				{
					m_left.GenerateList(list, traversal);
				}
				list.Add(this);
				if (m_right != null)
				{
					m_right.GenerateList(list, traversal);
				}
				
				break;
		}
	}

	// --------------------------------------------------------------------------------

	public void Clear()
	{
		if (m_left != null)
		{
			m_left.Clear();
			m_left = null;
		}

		if (m_right != null)
		{
			m_right.Clear();
			m_right = null;
		}
	}

	// --------------------------------------------------------------------------------

	public int GetDepth()
	{
		int depth = 1;
		BTreeNode<T> parent = m_parent;

		while (parent != null)
		{
			++depth;
			parent = parent.m_parent;
		}

		return depth;
	}

}