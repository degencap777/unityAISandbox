using UnityEngine;

// #SteveD >>> additional AI Agent commands:
//	- stop movement. check boxes for directional, rotational
//	- blind (disable sight)
//	- deaf (disable hearing)
//	- insensate (disable all sensory inputs)
//	x forget
//	- reset (reset to starting state)
//	- follow (attach debug camera)

public abstract class AIDiagnosticCommand : MonoBehaviour
{

	protected Agent m_owner = null;
	protected AIBrain m_brain = null;

	// --------------------------------------------------------------------------------

	protected abstract void Execute();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_owner = GetComponentInParent<Agent>();
		if (m_owner != null)
		{
			m_brain = m_owner.GetComponentInChildren<AIBrain>();
		}
	}

	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public void EditorExecute()
	{
		Execute();
	}

#endif // UNITY_EDITOR

}