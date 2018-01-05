using System;
using UnityEngine;

public class Goal_Evade : ScriptableObject, IGoal
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

	private Agent m_pursuant = null;
	public Agent Pursuant
	{
		get { return m_pursuant; }
		set { m_pursuant = value; }
	}

	// --------------------------------------------------------------------------------

	public bool IsAchieved()
	{
		// #SteveD >>> todo

		return false;
	}

	// --------------------------------------------------------------------------------

	public bool IsInvalidated()
	{
		// #SteveD >>> todo

		return false;
	}
	
}
