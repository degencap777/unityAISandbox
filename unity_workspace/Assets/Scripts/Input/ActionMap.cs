using System;
using System.Collections.Generic;
using UnityEngine;

namespace AISandbox.Input
{
	[Serializable]
	public class ActionMap : MonoBehaviour
	{

		[Serializable]
		private class KeyActionMapping : ActionMapping<BoundKeyState> { }
		[SerializeField]
		private List<KeyActionMapping> m_keyActionMappings = new List<KeyActionMapping>();

		[Serializable]
		private class AxisActionMapping : ActionMapping<BoundAxisState> { }
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
}