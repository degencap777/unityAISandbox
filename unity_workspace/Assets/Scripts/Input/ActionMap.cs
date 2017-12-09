﻿using System.Collections.Generic;
using UnityEngine;

public class ActionMap : MonoBehaviour
{

	[SerializeField]
	private List<Actionmapping> m_actionMappings = new List<Actionmapping>();

	// -----------------------------------------------------------------------------------

	public void Update()
	{
		int count = m_actionMappings.Count;
		if (m_actionMappings == null || count == 0)
		{
			return;
		}

		for (int i = 0; i < count; ++i)
		{
			m_actionMappings[i].Update();
		}
	}

}
