using System;
using UnityEngine;

public class Tree<T> where T : IComparable<T>
{

	private Node<T> m_root = null;

	// --------------------------------------------------------------------------------

	public void Insert(T data)
	{
		Insert(new Node<T>(data));
	}

	// --------------------------------------------------------------------------------

	private void Insert(Node<T> node)
	{
		// handle invalid node
		if (node == null)
		{
			return;
		}

		// insert into empty tree
		if (m_root == null)
		{
			m_root = node;
			return;
		}

		// find insertion point
		Node<T> current = m_root;
		Node<T> parent = current;
		while (current != null)
		{
			current = node.Data.CompareTo(current.Data) < 0 ? current.Left : current.Right;
			parent = current;
		}

		if (node.Data.CompareTo(parent.Data) < 0)
		{
			parent.Left = node;
		}
		else
		{
			parent.Right = node;
		}
	}

	// --------------------------------------------------------------------------------

	public bool Remove(T data)
	{
		return Remove(data, m_root);
	}

	// --------------------------------------------------------------------------------

	private bool Remove(T data, Node<T> current)
	{
		if (current == null)
		{
			return false;
		}

		if (data.CompareTo(current.Data) == 0)
		{
			// #SteveD	>>> detach current from tree

			// reinsert children
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
