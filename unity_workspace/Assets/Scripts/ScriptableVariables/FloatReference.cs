using System;
using UnityEngine;

[Serializable]
public class FloatReference
{

	// #SteveD	>>> property drawer:
	// - toggle constant/variable bool using VariableReferenceType enum dropdown
	// - single line
	// - hide whichever property isn't being used

	[SerializeField]
	private bool m_useVariable = true;

	[SerializeField]
	private float m_variable = 0.0f;

	[SerializeField]
	private FloatScriptable m_scriptable = null;

	// --------------------------------------------------------------------------------

	public float Value 
	{ 
		get 
		{ 
			return m_useVariable ? m_variable : m_scriptable.Value; 
		} 

		set 
		{
			if (m_useVariable) 
			{ 
				m_variable = value; 
			}
			else 
			{ 
				m_scriptable.Value = value; 
			}
		}
	}

}