using UnityEngine;

namespace AISandbox.Test
{
	public abstract class Test : MonoBehaviour
	{

		[SerializeField]
		private KeyCode m_executeKey = KeyCode.None;

		// --------------------------------------------------------------------------------

		protected abstract void RunTests();
		protected abstract void ResetTests();

		// --------------------------------------------------------------------------------

		protected virtual void Awake()
		{
			;
		}

		// --------------------------------------------------------------------------------

		protected virtual void Update()
		{
			if (m_executeKey != KeyCode.None && UnityEngine.Input.GetKeyUp(m_executeKey))
			{
				RunAndReset();
			}
		}

		// --------------------------------------------------------------------------------

		private void RunAndReset()
		{
			RunTests();
			ResetTests();
		}

		// --------------------------------------------------------------------------------

#if UNITY_EDITOR

		public void EditorRunAndReset()
		{
			RunAndReset();
		}

#endif // UNITY_EDITOR

	}
}