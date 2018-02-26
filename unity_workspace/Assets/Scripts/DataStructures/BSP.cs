using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{

	// #SteveD	>>> update >>> check for agents moving between partitions

	[SerializeField]
	private Vector3 m_minBounds = Vector3.zero;
	[SerializeField]
	private Vector3 m_maxBounds = Vector3.zero;

	[SerializeField]
	private int m_maximumDepth = 4;

	[SerializeField, Tooltip("The maximum number of agents that can occupy a partition before triggering division")]
	private int m_partitionAgentLimit = 8;
	
	// --------------------------------------------------------------------------------

	private BTree<BSPPartition> m_bsp = new BTree<BSPPartition>();
	private List<BTreeNode<BSPPartition>> m_subdivideChecks = new List<BTreeNode<BSPPartition>>();
	
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private int m_currentPartitionIndex = 0;

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
		if (agent == null || agent.Transform == null)
		{
			return;
		}

		BTreeNode<BSPPartition> node = FindNodeForPosition(agent.Transform.position);
		node.Data.AddAgent(agent);

		m_subdivideChecks.Clear();
		m_subdivideChecks.Add(node);
		
		while (m_subdivideChecks.Count > 0)
		{
			int lastIndex = m_subdivideChecks.Count - 1;
			node = m_subdivideChecks[lastIndex];
			m_subdivideChecks.RemoveAt(lastIndex);

			if (node.Data.AgentCount > m_partitionAgentLimit &&
				node.GetDepth() < m_maximumDepth)
			{
				SubdivideNode(node);

				var agentsEnumerator = node.Data.AgentsEnumerator;
				while (agentsEnumerator.MoveNext())
				{
					AddAgentToChild(node, agentsEnumerator.Current);
				}
				node.Data.FlushAgents();

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

	private static void AddAgentToChild(BTreeNode<BSPPartition> parent, Agent agent)
	{
		if (parent.Left.Data.ContainsPoint(agent.Transform.position))
		{
			parent.Left.Data.AddAgent(agent);
		}
		else if (parent.Right.Data.ContainsPoint(agent.Transform.position))
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
		var node = FindNodeForPosition(agent.Transform.position);
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
		var partitions = m_bsp.ToList(TreeTraversal.BreadthFirst);
		if (partitions.Count == 0)
		{
			return;
		}

		BSPPartition currentPartition = null;

		if (m_currentPartitionIndex < 0)
		{
			m_currentPartitionIndex = partitions.Count - 1;
		}
		else if (m_currentPartitionIndex >= partitions.Count)
		{
			m_currentPartitionIndex = 0;
		}

		currentPartition = partitions[m_currentPartitionIndex];
		Vector3 min = currentPartition.MinBounds;
		Vector3 max = currentPartition.MaxBounds;
		Vector3 size = max - min;

		Color cachedColour = Gizmos.color;
		Gizmos.color = Color.green;
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

		var agentsEnumerator = currentPartition.AgentsEnumerator;
		while (agentsEnumerator.MoveNext())
		{
			// #SteveD	>>> draw icon depicting agent in current partition
		}
		
		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}
