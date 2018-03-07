using System;
using System.Collections.Generic;
using UnityEngine;

public class BSPPartition : IComparable<BSPPartition>
{
	
	private Vector3 m_minBounds = Vector3.zero;
	public Vector3 MinBounds { get { return m_minBounds; } }

	private Vector3 m_maxBounds = Vector3.zero;
	public Vector3 MaxBounds { get { return m_maxBounds; } }

	private List<Agent> m_agents = new List<Agent>();
	public int AgentCount { get { return m_agents.Count; } }
	public IEnumerator<Agent> AgentsEnumerator { get { return m_agents.GetEnumerator(); } }

	// --------------------------------------------------------------------------------

	public BSPPartition(Vector3 minBounds, Vector3 maxBounds)
	{
		m_minBounds = minBounds;
		m_maxBounds = maxBounds;
	}

	// --------------------------------------------------------------------------------

	public void AddAgent(Agent agent)
	{
		if (agent == null)
		{
			Logger.Instance.Log(GetType().ToString(), LogLevel.Error, "Attempting to add a null Agent");
			return;
		}

		Vector3 agentPosition = agent.Position;
		Debug.AssertFormat(agentPosition.x >= m_minBounds.x, "[BSPPartition::AddAgent] Agent is not within minimum bounds (x) Agent.x: {0}, MinBounds.X: {1}\n", agentPosition.x, m_minBounds.x);
		Debug.AssertFormat(agentPosition.y >= m_minBounds.y, "[BSPPartition::AddAgent] Agent is not within minimum bounds (y) Agent.y: {0}, MinBounds.y: {1}\n", agentPosition.y, m_minBounds.y);
		Debug.AssertFormat(agentPosition.z >= m_minBounds.z, "[BSPPartition::AddAgent] Agent is not within minimum bounds (z) Agent.z: {0}, MinBounds.z: {1}\n", agentPosition.z, m_minBounds.z);
		Debug.AssertFormat(agentPosition.x <= m_maxBounds.x, "[BSPPartition::AddAgent] Agent is not within maximum bounds (x) Agent.x: {0}, MaxBounds.X: {1}\n", agentPosition.x, m_maxBounds.x);
		Debug.AssertFormat(agentPosition.y <= m_maxBounds.y, "[BSPPartition::AddAgent] Agent is not within maximum bounds (y) Agent.y: {0}, MaxBounds.y: {1}\n", agentPosition.y, m_maxBounds.y);
		Debug.AssertFormat(agentPosition.z <= m_maxBounds.z, "[BSPPartition::AddAgent] Agent is not within maximum bounds (z) Agent.z: {0}, MaxBounds.z: {1}\n", agentPosition.z, m_maxBounds.z);
		Debug.Assert(m_agents.Contains(agent) == false, "[BSPPartition::AddAgent] Agent is already in this partition\n");
		
		m_agents.Add(agent);
	}

	// --------------------------------------------------------------------------------

	public void RemoveAgent(Agent agent)
	{
		Debug.Assert(m_agents.Contains(agent), "[BSPPartition::RemoveAgent] Agent is not in this partition\n");

		m_agents.Remove(agent);
	}

	// --------------------------------------------------------------------------------

	public bool ContainsAgent(Agent agent)
	{
		return m_agents.Contains(agent);
	}

	// --------------------------------------------------------------------------------

	public bool ContainsAgentPosition(Agent agent)
	{
		return ContainsPoint(agent.Position);
	}

	// --------------------------------------------------------------------------------

	public bool ContainsPoint(Vector3 point)
	{
		return point.x >= m_minBounds.x && point.x <= m_maxBounds.x &&
			point.y >= m_minBounds.y && point.y <= m_maxBounds.y &&
			point.z >= m_minBounds.z && point.z <= m_maxBounds.z;
	}

	// --------------------------------------------------------------------------------

	public void ClearAgents()
	{
		m_agents.Clear();
	}

	// --------------------------------------------------------------------------------

	public int CompareTo(BSPPartition other)
	{
		Vector3 centre = m_minBounds + ((m_maxBounds - m_minBounds) * 0.5f);
		Vector3 otherCentre = other.m_minBounds + ((other.m_maxBounds - other.m_minBounds) * 0.5f);

		float diffX = centre.x - otherCentre.x;
		if (Mathf.Abs(diffX) > float.Epsilon)
		{
			return diffX < 0.0f ? -1 : 1;
		}

		float diffY = centre.y - otherCentre.y;
		if (Mathf.Abs(diffY) > float.Epsilon)
		{
			return diffY < 0.0f ? -1 : 1;
		}

		float diffZ = centre.z - otherCentre.z;
		if (Mathf.Abs(diffZ) > float.Epsilon)
		{
			return diffZ < 0.0f ? -1 : 1;
		}

		return 0;
	}

	// --------------------------------------------------------------------------------

	public override string ToString()
	{
		return string.Format("Min: ({0}, {1}, {2}), Max: ({3}, {4}, {5})",
			m_minBounds.x.ToString("0.00"), m_minBounds.y.ToString("0.00"), m_minBounds.z.ToString("0.00"),
			m_maxBounds.x.ToString("0.00"), m_maxBounds.y.ToString("0.00"), m_maxBounds.z.ToString("0.00"));
	}

}