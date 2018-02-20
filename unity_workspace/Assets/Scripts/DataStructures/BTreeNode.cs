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

	public void GenerateList(List<T> list)
	{
		if (m_left != null)
		{
			m_left.GenerateList(list);
		}

		list.Add(m_data);

		if (m_right != null)
		{
			m_right.GenerateList(list);
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

}