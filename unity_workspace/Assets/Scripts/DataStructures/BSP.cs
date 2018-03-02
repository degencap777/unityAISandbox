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

	[SerializeField]
	private Agent m_highlightedAgent = null;

	// --------------------------------------------------------------------------------

	private BTree<BSPPartition> m_bsp = new BTree<BSPPartition>();

	// worker lists
	private List<BTreeNode<BSPPartition>> m_subdivideChecks = new List<BTreeNode<BSPPartition>>();
	private List<Agent> m_migratingAgents = new List<Agent>();
	private List<BTreeNode<BSPPartition>> m_mergePartitions = new List<BTreeNode<BSPPartition>>();

	// --------------------------------------------------------------------------------

#if UNITY_EDITOR
		
	protected virtual void OnValidate()
	{
		m_partitionSplitLimit = Mathf.Clamp(m_partitionSplitLimit, 1, int.MaxValue);
		m_partitionCombineLimit = Mathf.Clamp(m_partitionCombineLimit, 0, m_partitionSplitLimit);
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
		UpdateMigration();
		TryMergePartitions();
	}

	// --------------------------------------------------------------------------------
	
	private void UpdateMigration()
	{
		var partitionsEnumerator = m_bsp.Enumerator(TreeTraversal.BreadthFirst);
		while (partitionsEnumerator.MoveNext())
		{
			// construct list of agents that will migrate from each partition
			var agentsEnumerator = partitionsEnumerator.Current.Data.AgentsEnumerator;
			while (agentsEnumerator.MoveNext())
			{
				if (partitionsEnumerator.Current.Data.ContainsAgentPosition(agentsEnumerator.Current) == false)
				{
					m_migratingAgents.Add(agentsEnumerator.Current);
				}
			}

			// migrate agents out of partition and re-insert into BSP
			for (int i = 0; i < m_migratingAgents.Count; ++i)
			{
				partitionsEnumerator.Current.Data.RemoveAgent(m_migratingAgents[i]);
				AddAgent(m_migratingAgents[i]);
			}
			m_migratingAgents.Clear();
		}
	}

	// --------------------------------------------------------------------------------
	
	private void TryMergePartitions()
	{
		var partitionsEnumerator = m_bsp.Enumerator(TreeTraversal.BreadthFirst);
		while (partitionsEnumerator.MoveNext())
		{
			var left = partitionsEnumerator.Current.Left;
			var right = partitionsEnumerator.Current.Right;

			// only merge leaf partitions
			if (left != null && left.Left == null && left.Right == null &&
				right != null && right.Left == null && right.Right == null)
			{
				int agentCount = partitionsEnumerator.Current.Left.Data.AgentCount +
					partitionsEnumerator.Current.Right.Data.AgentCount;

				if (agentCount <= m_partitionCombineLimit)
				{
					m_mergePartitions.Add(partitionsEnumerator.Current);
				}
			}
		}

		for (int i = 0; i < m_mergePartitions.Count; ++i)
		{
			CombineChildren(m_mergePartitions[i]);
		}
		m_mergePartitions.Clear();
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

	private void CombineChildren(BTreeNode<BSPPartition> parent)
	{
		Debug.Assert(parent != null, "[BSP::CombineNodes] parent is null");
		if (parent == null)
		{
			return;
		}

		var left = parent.Left;
		var right = parent.Right;

		Debug.Assert(left != null, "[BSP::CombineNodes] left is null");
		Debug.Assert(right != null, "[BSP::CombineNodes] right is null");
		if (left == null || right == null)
		{
			return;
		}
		
		var agentsEnumerator = left.Data.AgentsEnumerator;
		while (agentsEnumerator.MoveNext())
		{
			parent.Data.AddAgent(agentsEnumerator.Current);
		}
		left.Data.ClearAgents();

		agentsEnumerator = right.Data.AgentsEnumerator;
		while (agentsEnumerator.MoveNext())
		{
			parent.Data.AddAgent(agentsEnumerator.Current);
		}
		right.Data.ClearAgents();

		parent.Clear();
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
		Gizmos.color = Color.red;

		BSPPartition highlightedPartition = null;

		// draw all partitions
		var partitionsEnumerator = m_bsp.Enumerator(TreeTraversal.BreadthFirst);
		while (partitionsEnumerator.MoveNext())
		{
			var current = partitionsEnumerator.Current;

			if (m_highlightedAgent != null && highlightedPartition == null)
			{
				var agentsEnumerator = current.Data.AgentsEnumerator;
				while (agentsEnumerator.MoveNext())
				{
					if (agentsEnumerator.Current == m_highlightedAgent)
					{
						highlightedPartition = current.Data;
						break;
					}
				}
			}

			Gizmos_DrawPartition(partitionsEnumerator.Current.Data);
		}

		// draw partition for the agent we are tracking
		if (highlightedPartition != null)
		{
			Gizmos.color = Color.green;
			Gizmos_DrawPartition(highlightedPartition);
		}

		Gizmos.color = cachedColour;
	}

	// --------------------------------------------------------------------------------

	private void Gizmos_DrawPartition(BSPPartition partition)
	{
		Vector3 min = partition.MinBounds;
		Vector3 max = partition.MaxBounds;
		Vector3 size = max - min;

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

#endif // UNITY_EDITOR

}
