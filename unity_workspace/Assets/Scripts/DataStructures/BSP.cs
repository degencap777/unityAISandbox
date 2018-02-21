using UnityEngine;

public class BSP : MonoBehaviour
{

	// #SteveD	>>> update >>> check for agents moving between partitions

	[SerializeField]
	private Vector3 m_minBounds = Vector3.zero;
	[SerializeField]
	private Vector3 m_maxBounds = Vector3.zero;
	
	[SerializeField]
	private int m_partitionAgentLimit = 8;
	
	// --------------------------------------------------------------------------------

	private BTree<BSPPartition> m_bsp = new BTree<BSPPartition>();

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

	public void AddAgent(Agent agent)
	{
		Debug.Assert(agent != null && agent.Transform != null, "[BSP::AddAgent] Attempted to add an invalid agent\n");
		if (agent == null || agent.Transform == null)
		{
			return;
		}

		var node = FindNodeForPosition(agent.Transform.position);
		if (node.Data.AgentCount > m_partitionAgentLimit)
		{
			SubdivideNode(node);
			AddAgentToChild(node, agent);
			
			var agentsEnumerator = node.Data.AgentsEnumerator;
			while (agentsEnumerator.MoveNext())
			{
				AddAgentToChild(node, agentsEnumerator.Current);
			}
			node.Data.FlushAgents();
			
			// #SteveD	>>> handle node.Left or node.Right being over capacity

		}
		else
		{
			node.Data.AddAgent(agent);
		}

		m_bsp.Log(TreeTraversal.BreadthFirst);
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
	
	private void OnDrawGizmos()
	{
		Color cachedColour = Gizmos.color;
		Gizmos.color = Color.green;

		var partitions = m_bsp.ToList(TreeTraversal.BreadthFirst);
		for (int i = 0; i < partitions.Count; ++i)
		{
			Vector3 min = partitions[i].MinBounds;
			Vector3 max = partitions[i].MaxBounds;
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
		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}
