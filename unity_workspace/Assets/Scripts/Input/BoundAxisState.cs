using System;
using UnityEngine;

[Serializable]
public class BoundAxisState : IBoundInputState
{

	[SerializeField]
	private string m_requiredAxis = string.Empty;

	[SerializeField]
	private float m_minimumThreshold = 0.2f;
	
	// --------------------------------------------------------------------------------

	public void Update()
	{
		;
	}

	// --------------------------------------------------------------------------------
	
	public bool ConditionsMet()
	{
		float axis = Input.GetAxis(m_requiredAxis);
		return (m_minimumThreshold > 0.0f && axis >= m_minimumThreshold) || 
			(m_minimumThreshold < 0.0f && axis <= m_minimumThreshold);
	}

}
