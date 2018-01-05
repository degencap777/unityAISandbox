using System;
using UnityEngine;

public class Goal_Pursue : ScriptableObject, IGoal
{

	private float m_successDistance = 5.0f;
	public float SuccessDistance
	{
		get { return m_successDistance; }
		set { m_successDistance = value; }
	}

	// --------------------------------------------------------------------------------

	private float m_activateDistance = 10.0f;
	public float ActivateDistance
	{
		get { return m_activateDistance; }
		set { m_activateDistance = value; }
	}

	// --------------------------------------------------------------------------------

	private Agent m_target = null;
	public Agent Target
	{
		get { return m_target; }
		set { m_target = value; }
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
