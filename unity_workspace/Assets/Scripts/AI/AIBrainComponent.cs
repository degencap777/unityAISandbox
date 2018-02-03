using UnityEngine;

public abstract class AIBrainComponent : MonoBehaviour
{
	
	protected AIBrain m_brain = null;
	
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

		Debug.Assert(m_brain != null, "[AIBrainComponent::Awake] GetComponent<AIBrain> failed\n");

		OnAwake();
	}

	// --------------------------------------------------------------------------------

	protected abstract void OnAwake();
	public abstract void OnStart();
	public abstract void OnUpdate();

}
