using System;
using UnityEngine;

namespace AISandbox.Input
{
	[Serializable]
	public class BoundAxisState : BoundInputState
	{

		[SerializeField]
		private string m_requiredAxis = string.Empty;

		[SerializeField]
		private float m_deadZone = 0.2f;

		// --------------------------------------------------------------------------------

		private float m_currentValue = 0.0f;

		// --------------------------------------------------------------------------------

		public override void Update()
		{
			m_currentValue = UnityEngine.Input.GetAxis(m_requiredAxis);
		}

		// --------------------------------------------------------------------------------

		public override bool ConditionsMet()
		{
			return Mathf.Abs(m_currentValue) > m_deadZone;
		}

		// --------------------------------------------------------------------------------

		public override float GetValue()
		{
			return m_currentValue;
		}

	}
}