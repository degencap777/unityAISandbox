using AISandbox.Character;
using System;
using UnityEngine;

namespace AISandbox.Input
{
	[Serializable]
	public class ActionMapping<T> where T : BoundInputState
	{

		[SerializeField]
		private string m_name = string.Empty;
		public string Name { get { return m_name; } }

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
				m_action.Execute(m_boundInputState.GetValue());
			}
		}

	}
}