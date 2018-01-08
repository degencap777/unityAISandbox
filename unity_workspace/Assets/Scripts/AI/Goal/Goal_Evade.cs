using System;
using UnityEngine;

public class Goal_Evade : Goal
{

	private float m_successDistanceSquared = 25.0f;
	public float SuccessDistanceSquared
	{
		get { return m_successDistanceSquared; }
		set { m_successDistanceSquared = value; }
	}

	// --------------------------------------------------------------------------------

	public override bool IsAchieved()
	{
		Agent owner = GetOwner();
		Agent target = GetHighestPriorityTarget();

		if (owner != null && target != null)
		{
			Vector3 ownerPosition = owner.transform.position;
			Vector3 targetPosition = target.transform.position;
			Vector3 toTarget = targetPosition - ownerPosition;
			return toTarget.sqrMagnitude >= m_successDistanceSquared;
		}

		return false;
	}

	// --------------------------------------------------------------------------------

	public override bool IsInvalidated()
	{
		return GetHighestPriorityTarget() == null;
	}

}
