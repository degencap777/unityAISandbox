using System;
using UnityEngine;

public class Goal_Evade : Goal
{

	private float m_successDistance = 25.0f;
	public float SuccessDistance
	{
		get { return m_successDistance; }
		set { m_successDistance = value; }
	}

	// --------------------------------------------------------------------------------

	private float m_activateDistance = 23.0f;
	public float ActivateDistance
	{
		get { return m_activateDistance; }
		set { m_activateDistance = value; }
	}
	
	// --------------------------------------------------------------------------------

	public override bool IsAchieved()
	{
		Agent target = GetHighestPriorityTarget();
		if (target != null)
		{
			// #SteveD >>>> todo
		}

		return false;
	}

	// --------------------------------------------------------------------------------

	public override bool IsInvalidated()
	{
		Agent target = GetHighestPriorityTarget();
		if (target != null)
		{
			// #SteveD >>>> todo
		}

		return false;
	}

}
