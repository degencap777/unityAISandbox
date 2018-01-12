using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BehaviourCollection
{

	private enum BehaviourId { Pursue, Evade }

	// #SteveD >>> make this a bitflag instead of a list
	[SerializeField]
	private List<BehaviourId> m_availableBehaviours = new List<BehaviourId>();

	private List<Behaviour> m_behaviours = new List<Behaviour>();
	private int m_currentBehaviourIndex = 0;

	// --------------------------------------------------------------------------------

	private bool IsCurrentBehaviourIndexValid()
	{
		return m_currentBehaviourIndex >= 0 && m_currentBehaviourIndex < m_behaviours.Count;
	}

	// --------------------------------------------------------------------------------

	public BehaviourCollection()
	{
	}

	// --------------------------------------------------------------------------------

	public void Initialise(Agent owner, Memory memory)
	{
		// #SteveD >>> set up using m_availableBehaviours
		m_behaviours.Add(new Behaviour_Pursue(owner, memory));
		m_behaviours.Add(new Behaviour_Evade(owner, memory));

		if (IsCurrentBehaviourIndexValid())
		{
			m_behaviours[m_currentBehaviourIndex].OnEnter();
		}
	}

	// --------------------------------------------------------------------------------
	
	public void OnUpdate()
	{
		if (IsCurrentBehaviourIndexValid())
		{
			m_behaviours[m_currentBehaviourIndex].OnUpdate();
		}
	}

}
