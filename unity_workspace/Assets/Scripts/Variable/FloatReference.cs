using System;
using UnityEngine;

[Serializable]
public class FloatReference
{

	[SerializeField]
	private bool m_useValue = true;

	[SerializeField]
	private float m_value = 0.0f;

	[SerializeField]
	private FloatVariable m_variable = null;

	// --------------------------------------------------------------------------------

	public float Value 
	{ 
		get 
		{
			if (m_useValue)
			{
				return m_value;
			}
			else
			{
				return m_variable != null ? m_variable.Value : 0.0f;
			}
		} 

		set 
		{
			if (m_useValue) 
			{ 
				m_value = value; 
			}
			else if (m_variable != null)
			{
				m_variable.Value = value;
			}
		}
	}

}