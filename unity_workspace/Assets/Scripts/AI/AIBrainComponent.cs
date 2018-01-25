using UnityEngine;

public abstract class AIBrainComponent : MonoBehaviour
{

	private AIBrain m_brain = null;

	// --------------------------------------------------------------------------------

	protected Agent Owner { get { return m_brain != null ? m_brain.Owner : null; } }
	protected WorkingMemory WorkingMemory { get { return m_brain != null ? m_brain.WorkingMemory : null; } }
	protected AIBehaviourCollection Behaviours { get { return m_brain != null ? m_brain.Behaviours : null; } }
	protected Perception_Visual Sight { get { return m_brain != null ? m_brain.Sight : null; } }
	protected Perception_Audible Hearing { get { return m_brain != null ? m_brain.Hearing : null; } }
	
	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_brain = GetComponentInChildren<AIBrain>();	
	}
	
}
