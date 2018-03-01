using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{

	[SerializeField]
	private Vector3 m_minBounds = Vector3.zero;
	[SerializeField]
	private Vector3 m_maxBounds = Vector3.zero;

	[SerializeField]
	private int m_maximumDepth = 4;

	[SerializeField, Tooltip("The maximum number of agents that can occupy a partition before triggering division")]
	private int m_partitionSplitLimit = 8;

	[SerializeField, Tooltip("The maximum number of agents that can occupy a partition before triggering division")]
	private int m_partitionCombineLimit = 6;

	// --------------------------------------------------------------------------------

	private BTree<BSPPartition> m_bsp = new BTree<BSPPartition>();
	private List<BTreeNode<BSPPartition>> m_subdivideChecks = new List<BTreeNode<BSPPartition>>();
	
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private int m_currentPartitionIndex = 0;

	// --------------------------------------------------------------------------------

	protected virtual void OnValidate()
	{
		if (m_partitionCombineLimit >= m_partitionSplitLimit)
		{
			m_partitionCombineLimit = m_partitionSplitLimit - 1;
		}
	}

#endif // UNITY_EDITOR

	// --------------------------------------------------------------------------------

	protected virtual void Start()
	{
		m_bsp.Insert(new BSPPartition(m_minBounds, m_maxBounds));

		var agents = FindObjectsOfType<Agent>();
		for (int i = 0; i < agents.Length; ++i)
		{
			AddAgent(agents[i]);
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		var partitionsEnumerator = m_bsp.Enumerator(TreeTraversal.BreadthFirst);
		while (partitionsEnumerator.MoveNext())
		{
			var agentsEnumerator = partitionsEnumerator.Current.Data.AgentsEnumerator;
			while (agentsEnumerator.MoveNext())
			{
				if (partitionsEnumerator.Current.Data.ContainsAgent(agentsEnumerator.Current) == false)
				{
					partitionsEnumerator.Current.Data.RemoveAgent(agentsEnumerator.Current);
					AddAgent(agentsEnumerator.Current);
				}
			}
		}

		partitionsEnumerator = m_bsp.Enumerator(TreeTraversal.BreadthFirst);
		while (partitionsEnumerator.MoveNext())
		{
			var left = partitionsEnumerator.Current.Left;
			var right = partitionsEnumerator.Current.Right;

			if (left != null && left.Left == null && left.Right == null &&
				right != null && right.Left == null && right.Right == null)
			{
				int agentCount = partitionsEnumerator.Current.Left.Data.AgentCount +
					partitionsEnumerator.Current.Right.Data.AgentCount;

				if (agentCount <= m_partitionCombineLimit)
				{
					CombineNodes(left, right);
				}
			}
		}

#if UNITY_EDITOR

		if (Input.GetKeyUp(KeyCode.Comma))
		{
			--m_currentPartitionIndex;
		}
		else if (Input.GetKeyUp(KeyCode.Period))
		{
			++m_currentPartitionIndex;
		}

#endif // UNITY_EDITOR
	}

// --------------------------------------------------------------------------------

public void AddAgent(Agent agent)
	{
		Debug.Assert(agent != null && agent.Transform != null, "[BSP::AddAgent] Attempted to add an invalid agent\n");
		if (agent == null)
		{
			return;
		}

		BTreeNode<BSPPartition> node = FindNodeForPosition(agent.Position);
		node.Data.AddAgent(agent);

		m_subdivideChecks.Clear();
		m_subdivideChecks.Add(node);
		
		while (m_subdivideChecks.Count > 0)
		{
			int lastIndex = m_subdivideChecks.Count - 1;
			node = m_subdivideChecks[lastIndex];
			m_subdivideChecks.RemoveAt(lastIndex);

			if (node.Data.AgentCount > m_partitionSplitLimit &&
				node.GetDepth() < m_maximumDepth)
			{
				SubdivideNode(node);

				var agentsEnumerator = node.Data.AgentsEnumerator;
				while (agentsEnumerator.MoveNext())
				{
					AddAgentToChild(node, agentsEnumerator.Current);
				}
				node.Data.ClearAgents();

				m_subdivideChecks.Add(node.Left);
				m_subdivideChecks.Add(node.Right);
			}
		}
	}

	// --------------------------------------------------------------------------------

	private void SubdivideNode(BTreeNode<BSPPartition> node)
	{
		Vector3 size = node.Data.MaxBounds - node.Data.MinBounds;
		float halfX = size.x * 0.5f;
		float halfZ = size.z * 0.5f;

		Vector3 leftMaxBounds = node.Data.MaxBounds;
		Vector3 rightMinBounds = node.Data.MinBounds;

		if (halfX >= halfZ) // split on YZ plane 
		{
			leftMaxBounds.x -= halfX;
			rightMinBounds.x += halfX;
		}
		else // split on XY plane
		{
			leftMaxBounds.z -= halfZ;
			rightMinBounds.z += halfZ;
		}

		node.Insert(new BTreeNode<BSPPartition>(new BSPPartition(node.Data.MinBounds, leftMaxBounds), node));
		node.Insert(new BTreeNode<BSPPartition>(new BSPPartition(rightMinBounds, node.Data.MaxBounds), node));
		Debug.Assert(node.Left != null && node.Right != null, "[BSP::SubdivideNode] failed to divide node into 2 children");
	}

	// --------------------------------------------------------------------------------

	private void CombineNodes(BTreeNode<BSPPartition> left, BTreeNode<BSPPartition> right)
	{
		Debug.Assert(left != null, "[BSP::CombineNodes] left is null");
		Debug.Assert(left.Parent != null, "[BSP::CombineNodes] left.Parent is null");
		Debug.Assert(right != null, "[BSP::CombineNodes] right is null");
		Debug.Assert(right.Parent != null, "[BSP::CombineNodes] right.Parent is null");

		if (left == null || right == null)
		{
			return;
		}

		if (left.Parent == null || right.Parent == null)
		{
			return;
		}

		var agentsEnumerator = left.Data.AgentsEnumerator;
		while (agentsEnumerator.MoveNext())
		{
			left.Parent.Data.AddAgent(agentsEnumerator.Current);
		}
		left.Data.ClearAgents();

		agentsEnumerator = right.Data.AgentsEnumerator;
		while (agentsEnumerator.MoveNext())
		{
			right.Parent.Data.AddAgent(agentsEnumerator.Current);
		}
		right.Data.ClearAgents();
	}

	// --------------------------------------------------------------------------------

	private static void AddAgentToChild(BTreeNode<BSPPartition> parent, Agent agent)
	{
		if (agent == null)
		{
			return;
		}

		if (parent.Left.Data.ContainsPoint(agent.Position))
		{
			parent.Left.Data.AddAgent(agent);
		}
		else if (parent.Right.Data.ContainsPoint(agent.Position))
		{
			parent.Right.Data.AddAgent(agent);
		}
		else
		{
			Debug.LogError("[BSP::AddAgent] Subdivide partition failed; agent cannot be inserted into either subdivision\n");
		}
	}

	// --------------------------------------------------------------------------------

	public void RemoveAgent(Agent agent)
	{
		var node = FindNodeForPosition(agent.Position);
		if (node != null)
		{
			Debug.Assert(node.Data.ContainsAgent(agent), "[BSP::RemoveAgent] Agent is not in suggested partition\n");
			node.Data.RemoveAgent(agent);
		}
	}

	// --------------------------------------------------------------------------------

	private BTreeNode<BSPPartition> FindNodeForPosition(Vector3 position)
	{
		var current = m_bsp.Root;
		while (current != null)
		{
			if (current.Left == null && current.Right == null)
			{
				return current;
			}
			else if (current.Left == null || current.Right == null)
			{
				Debug.LogError("[BSP::FindNodeForPosition] Invalid BSP tree; nodes should have either 0 or 2 children, found a node with 1 child\n");
				return null;
			}
			else
			{
				if (current.Left.Data.ContainsPoint(position))
				{
					current = current.Left;
				}
				else if (current.Right.Data.ContainsPoint(position))
				{
					current = current.Right;
				}
				else
				{
					Debug.LogErrorFormat("[BSP::FindNodeForPosition] No child nodes contain the agent position ({0})\n", position.ToString());
					return null;
				}
			}
		}
		return null;
	}

	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{		
		Color cachedColour = Gizmos.color;
		
		var partitionsEnumerator = m_bsp.Enumerator(TreeTraversal.BreadthFirst);
		while (partitionsEnumerator.MoveNext())
		{
			var current = partitionsEnumerator.Current;

			Vector3 min = current.Data.MinBounds;
			Vector3 max = current.Data.MaxBounds;
			Vector3 size = max - min;

			Gizmos.color = Color.green;
			if (current.Parent != null)
			{
				Gizmos.color = current.Parent.Left == current ? Color.green : Color.red;
			}

			Gizmos.DrawLine(min, min + new Vector3(size.x, 0.0f, 0.0f));
			Gizmos.DrawLine(min, min + new Vector3(0.0f, size.y, 0.0f));
			Gizmos.DrawLine(min, min + new Vector3(0.0f, 0.0f, size.z));
			Gizmos.DrawLine(min + new Vector3(size.x, 0.0f, 0.0f), min + new Vector3(size.x, 0.0f, size.z));
			Gizmos.DrawLine(min + new Vector3(0.0f, size.y, 0.0f), min + new Vector3(0.0f, size.y, size.z));
			Gizmos.DrawLine(min + new Vector3(0.0f, 0.0f, size.z), min + new Vector3(size.x, 0.0f, size.z));
			Gizmos.DrawLine(min + new Vector3(0.0f, 0.0f, size.z), min + new Vector3(0.0f, size.y, size.z));
			Gizmos.DrawLine(max, max - new Vector3(size.x, 0.0f, 0.0f));
			Gizmos.DrawLine(max, max - new Vector3(0.0f, size.y, 0.0f));
			Gizmos.DrawLine(max, max - new Vector3(0.0f, 0.0f, size.z));
			Gizmos.DrawLine(max - new Vector3(0.0f, 0.0f, size.z), max - new Vector3(size.x, 0.0f, size.z));
			Gizmos.DrawLine(max - new Vector3(0.0f, 0.0f, size.z), max - new Vector3(0.0f, size.y, size.z));
		}
		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}
