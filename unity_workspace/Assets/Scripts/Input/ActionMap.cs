using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionMap : MonoBehaviour
{

	[SerializeField]
	private List<KeyActionMapping> m_keyActionMappings = new List<KeyActionMapping>();

	[SerializeField]
	private List<AxisActionMapping> m_axisActionMappings = new List<AxisActionMapping>();

	// -----------------------------------------------------------------------------------

	public void Update()
	{
		for (int i = 0; i < m_keyActionMappings.Count; ++i)
		{
			m_keyActionMappings[i].Update();
		}

		for (int i = 0; i < m_axisActionMappings.Count; ++i)
		{
			m_axisActionMappings[i].Update();
		}
	}

}
