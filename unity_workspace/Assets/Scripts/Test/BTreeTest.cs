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
		Debug.Log("[BTreeTest] Insert 5\n");
		m_tree.Insert(5);
		LogTree();
		Debug.Log("[BTreeTest] Insert 7\n");
		m_tree.Insert(7);
		LogTree();
		Debug.Log("[BTreeTest] Insert 8\n");
		m_tree.Insert(8);
		LogTree();
		Debug.Log("[BTreeTest] Insert 1\n");
		m_tree.Insert(1);
		LogTree();
		Debug.Log("[BTreeTest] Insert 3\n");
		m_tree.Insert(3);
		LogTree();
		Debug.Log("[BTreeTest] Insert 2\n");
		m_tree.Insert(2);
		LogTree();
		Debug.Log("[BTreeTest] Insert 4\n");
		m_tree.Insert(4);
		LogTree();
		Debug.Log("[BTreeTest] Insert 9\n");
		m_tree.Insert(9);
		LogTree();
		Debug.Log("[BTreeTest] Insert 0\n");
		m_tree.Insert(0);
		LogTree();
		Debug.Log("[BTreeTest] Insert 10\n");
		m_tree.Insert(10);
		LogTree();
		Debug.Log("[BTreeTest] Insert 6\n");
		m_tree.Insert(6);
		LogTree();
		Debug.Log("[BTreeTest] Remove 11\n");
		m_tree.Remove(11);
		LogTree();
		Debug.Log("[BTreeTest] Remove 7\n");
		m_tree.Remove(7);
		LogTree();
		Debug.Log("[BTreeTest] Remove 5\n");
		m_tree.Remove(5);
		LogTree();
		Debug.Log("[BTreeTest] Clear\n");
		m_tree.Clear();
		LogTree();
	}

	// --------------------------------------------------------------------------------

	private void LogTree()
	{
		Debug.LogFormat("[BTreeTest] {0}\n", string.Join(", ", m_tree.Content().ConvertAll<string>(n => n.ToString()).ToArray()));
	}

}
