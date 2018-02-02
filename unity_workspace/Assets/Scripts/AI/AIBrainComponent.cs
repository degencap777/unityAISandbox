using UnityEngine;

public abstract class AIBrainComponent : MonoBehaviour
{
	
	private AIBrain m_brain = null;

	// --------------------------------------------------------------------------------
	
	protected Agent Owner { get { return m_brain != null ? m_brain.Owner : null; } }
	protected WorkingMemory WorkMemory { get { return m_brain != null ? m_brain.WorkMemory : null; } }
	protected AIBehaviourController BehaviourController { get { return m_brain != null ? m_brain.BehaviourController : null; } }
	
	// --------------------------------------------------------------------------------

	private void Awake()
	{
		// check all parents until we find our brain, until we no longer have a 
		// parent or until we reach the gameObject containing the Agent component
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
