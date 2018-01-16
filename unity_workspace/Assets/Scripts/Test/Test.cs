using UnityEngine;

public abstract class Test : MonoBehaviour
{

	[SerializeField]
	private bool m_executeOnStart = false;

	// --------------------------------------------------------------------------------

	protected abstract void RunTests();
	protected abstract void ResetTests();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	private void Start()
	{
		if (m_executeOnStart)
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
