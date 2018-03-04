using UnityEngine;

public class BTreeTest : Test
{

	private BTree<int> m_tree = new BTree<int>();

	// --------------------------------------------------------------------------------

	protected override void ResetTests()
	{
		m_tree.Clear();
	}

	// --------------------------------------------------------------------------------

	protected override void RunTests()
	{
		m_tree.Insert(5);
		m_tree.Insert(7);
		m_tree.Insert(8);
		m_tree.Insert(1);
		m_tree.Insert(3);
		m_tree.Insert(2);
		m_tree.Insert(4);
		m_tree.Insert(9);
		m_tree.Insert(0);
		m_tree.Insert(10);
		m_tree.Insert(6);
		m_tree.Remove(11);
		m_tree.Remove(7);
		m_tree.Remove(5);
	}

}
