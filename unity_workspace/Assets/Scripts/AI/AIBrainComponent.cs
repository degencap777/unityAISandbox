using UnityEngine;

public abstract class AIBrainComponent : MonoBehaviour
{
	
	private AIBrain m_brain = null;

	// --------------------------------------------------------------------------------
	
	protected Agent Owner { get { return m_brain != null ? m_brain.Owner : null; } }
	protected WorkingMemory WorkMemory { get { return m_brain != null ? m_brain.WorkMemory : null; } }
	protected AIBehaviourController BehaviourController { get { return m_brain != null ? m_brain.BehaviourController : null; } }
	protected Perception_Visual Visual { get { return m_brain != null ? m_brain.VisualPerception : null; } }
	protected Perception_Audible Audible { get { return m_brain != null ? m_brain.AudiblePerception : null; } }
	
	// --------------------------------------------------------------------------------

	private void Awake()
	{
		// --------------------------------
		// AI Agent hierarchy
		//	- Agent
		//		- AIBrain
		//			- Behaviour [AIBehaviourCollection]
		//				- [AIBehaviour]
		//				- [AIBehvaiour]
		//				- ...
		//			- Memory [Working Memory, Historic Memory]
		//			- Perception [Visual, Audible]
		// --------------------------------

		// check all parents until we find our brain, we no longer have a parent or we reach the gameObject 
		// containing the Agent component
		Transform parent = transform.parent;
		while (m_brain == null && parent != null && parent.GetComponent<Agent>() == null)
		{
			m_brain = GetComponentInParent<AIBrain>();
			parent = parent.parent;
		}

		OnAwake();
	}

	// --------------------------------------------------------------------------------

	protected abstract void OnAwake();
	public abstract void OnStart();
	public abstract void OnUpdate();

}
