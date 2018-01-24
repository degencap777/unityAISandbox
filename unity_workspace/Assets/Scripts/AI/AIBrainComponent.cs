using UnityEngine;

public abstract class AIBrainComponent : MonoBehaviour
{

	private AIBrain m_brain = null;

	protected AIBrain WorkingMemory { get { return m_brain != null ? m_brain.WorkingMemory : null; } }
	protected AIBrain BehaviourCollection { get { return m_brain != null ? m_brain.BehaviourCollection : null; } }
	protected AIBrain Sight { get { return m_brain != null ? m_brain.Sight : null; } }
	protected AIBrain Hearing { get { return m_brain != null ? m_brain.AudiblePerception : null; } }
	
	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_brain = GetComponent<AIBrain>();	
	}
	
}
