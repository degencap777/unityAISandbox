using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Actionmapping
{

	[SerializeField]
	private BoundKeyState m_boundKey = new BoundKeyState();
	
	[SerializeField]
	private AgentAction m_action = null;
	
	// --------------------------------------------------------------------------------

	public void Update()
	{
		if (m_boundKey == null)
		{
			return;
		}

		m_boundKey.Update();
		if (m_boundKey.ConditionsMet())
		{
			m_action.Execute();
		}
	}

}
