using UnityEngine;

public class BSP : MonoBehaviour
{

	// #SteveD	>>> draw BSP (gizmo) >>> colour code for easy validation

	// #SteveD	>>> update >>> check for agents moving between partitions

	[SerializeField]
	private int m_partitionAgentLimit = 8;

	// --------------------------------------------------------------------------------

	private BTree<BSPPartition> m_bsp = new BTree<BSPPartition>();

	// --------------------------------------------------------------------------------

	public void AddAgent(Agent agent)
	{
		Debug.Assert(agent != null && agent.Transform != null, "[BSP::AddAgent] Attempted to add an invalid agent\n");
		if (agent == null || agent.Transform == null)
		{
			return;
		}

		var node = FindNodeForPosition(agent.Transform.position);
		if (node.Data.AgentCount >= m_partitionAgentLimit)
		{
			Vector3 size = node.Data.MaxBounds - node.Data.MinBounds;

			float halfX = size.x * 0.5f;
			float halfY = size.y * 0.5f;
			float halfZ = size.z * 0.5f;

			BSPPartition left = null;
			BSPPartition right = null;

			if (halfX >= halfY && halfX >= halfZ) // split on YZ plane 
			{
				// #SteveD	>>> left = new BSPPartition(???, ???);
				// #SteveD	>>> right = new BSPPartition(???, ???);
			}
			else if (halfY >= halfX && halfY >= halfZ) // split on XZ plane
			{
				// #SteveD	>>> left = new BSPPartition(???, ???);
				// #SteveD	>>> right = new BSPPartition(???, ???);
			}
			else // halfZ >= halfX && halfZ >= halfY, split on XY plane
			{
				// #SteveD	>>> left = new BSPPartition(???, ???);
				// #SteveD	>>> right = new BSPPartition(???, ???);
			}

			var agents = node.Data.Agents;
			agents.Add(agent);
			
			for (int i = 0; i < agents.Count; ++i)
			{
				if (left.ContainsPoint(agents[i].Transform.position))
				{
					left.AddAgent(agents[i]);
				}
				else if (right.ContainsPoint(agents[i].Transform.position))
				{
					right.AddAgent(agents[i]);
				}
				else
				{
					Debug.LogError("[BSP::AddAgent] Subdivide partition failed; agent cannot be inserted into either subdivision\n");
				}
			}
			node.Data.FlushAgents();

			// #SteveD	>>> insertion into tree requires comparison of BSPPartitions
			m_bsp.Insert(left);
			m_bsp.Insert(right);
			// <<<<<<<<<<<<
			
			// #SteveD	>>> handle all agents being added to one partition (one child being over limit)
		}
		else
		{
			node.Data.AddAgent(agent);
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

		// #SteveD	>>> check for partitions that can be combined
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

}
