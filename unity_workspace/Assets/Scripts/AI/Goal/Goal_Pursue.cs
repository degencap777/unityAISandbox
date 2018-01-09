using System;
using UnityEngine;

public class Goal_Pursue : Goal
{

	private float m_successDistanceSquared = 2.0f;
	public float SuccessDistanceSquared
	{
		get { return m_successDistanceSquared; }
		set { m_successDistanceSquared = value; }
	}

	// --------------------------------------------------------------------------------

	public override bool IsAchieved()
	{
		if (m_workingMemory == null)
		{
			return false;
		}

		Agent owner = m_workingMemory.Owner;
		Agent target = m_workingMemory.GetHighestPriorityTarget();

		if (owner != null && target != null)
		{
			Vector3 toTarget = target.transform.position - owner.transform.position;
			return toTarget.sqrMagnitude <= m_successDistanceSquared;
		}

		return false;
	}

	// --------------------------------------------------------------------------------

	public override bool IsInvalidated()
	{
		return m_workingMemory == null || m_workingMemory.GetHighestPriorityTarget() == null;
	}

}
