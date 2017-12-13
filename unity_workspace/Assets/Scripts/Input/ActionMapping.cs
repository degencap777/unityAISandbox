using System;
using UnityEngine;

public class ActionMapping<T> where T : BoundInputState
{

	[SerializeField]
	private T m_boundInputState = null;
	
	[SerializeField]
	private AgentAction m_action = null;
	
	// --------------------------------------------------------------------------------

	public void Update()
	{
		if (m_boundInputState == null)
		{
			return;
		}

		m_boundInputState.Update();
		if (m_boundInputState.ConditionsMet())
		{
			m_action.Execute();
		}
	}

}

// ------------------------------------------------------------------------------------

[Serializable]
public class KeyActionMapping : ActionMapping<BoundKeyState>
{
}

// ------------------------------------------------------------------------------------

[Serializable]
public class AxisActionMapping : ActionMapping<BoundAxisState>
{
}
