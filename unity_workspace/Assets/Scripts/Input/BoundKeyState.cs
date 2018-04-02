using System;
using UnityEngine;

namespace AISandbox.Input
{
	[Serializable]
	public class BoundKeyState : BoundInputState
	{

		[SerializeField]
		private KeyCode m_requiredKeyCode = KeyCode.None;

		[SerializeField]
		private KeyState m_requiredKeyState = KeyState.None;

		[SerializeField]
		private float m_requiredDuration = 0.0f;

		// --------------------------------------------------------------------------------

		private KeyState m_currentKeyState = KeyState.None;
		private float m_currentDuration = 0.0f;

		// --------------------------------------------------------------------------------

		public override void Update()
		{
			if (m_requiredKeyCode == KeyCode.None)
			{
				return;
			}

			var keyDown = UnityEngine.Input.GetKey(m_requiredKeyCode);

			switch (m_currentKeyState)
			{
				case KeyState.None:
					if (keyDown)
					{
						// None > Depressed
						m_currentKeyState = KeyState.Depressed;
						m_currentDuration = 0.0f;
					}
					break;

				case KeyState.Depressed:
					if (keyDown)
					{
						// Depressed > Held
						m_currentKeyState = KeyState.Held;
						m_currentDuration += Time.deltaTime;
					}
					else
					{
						// Depressed > Released
						m_currentKeyState = KeyState.Released;
					}
					break;

				case KeyState.Released:
					if (keyDown)
					{
						// Released > Depressed
						m_currentKeyState = KeyState.Depressed;
						m_currentDuration = 0.0f;
					}
					else
					{
						// Released > None
						m_currentKeyState = KeyState.None;
					}
					break;

				case KeyState.Held:
					if (keyDown)
					{
						// Held > Held
						m_currentDuration += Time.deltaTime;
					}
					else
					{
						// Held > Released
						m_currentKeyState = KeyState.Released;
					}
					break;
			}
		}

		// --------------------------------------------------------------------------------

		public override bool ConditionsMet()
		{
			switch (m_requiredKeyState)
			{
				case KeyState.None:         // fall through
				case KeyState.Depressed:    // fall through
				case KeyState.Released:
					return m_currentKeyState == m_requiredKeyState;

				case KeyState.Held:
					return m_currentKeyState == m_requiredKeyState &&
						m_requiredDuration <= m_currentDuration;
			}
			return false;
		}

		// --------------------------------------------------------------------------------

		public override float GetValue()
		{
			return 1.0f;
		}
	}
}