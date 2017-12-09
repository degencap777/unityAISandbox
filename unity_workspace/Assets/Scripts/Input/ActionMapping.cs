using System;
using UnityEngine;

[Serializable]
public class ActionMapping
{

	[SerializeField]
	private IBoundInputState m_boundInput = null;
	
	[SerializeField]
	private AgentAction m_action = null;
	
	// --------------------------------------------------------------------------------

	public void Update()
	{
		if (m_boundInput == null)
		{
			return;
		}

		m_boundInput.Update();
		if (m_boundInput.ConditionsMet())
		{
			m_action.Execute();
		}
	}

}
