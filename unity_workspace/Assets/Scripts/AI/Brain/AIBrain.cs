using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AIBrain : MonoBehaviour, IDistributedUpdatable
{

	private Agent m_owner = null;
	public Agent Owner { get { return m_owner; } }
	
	private List<AIBrainComponent> m_brainComponents = new List<AIBrainComponent>();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_owner = GetComponentInParent<Agent>();
	}

	// --------------------------------------------------------------------------------

	protected virtual void Start()
	{
		for (int i = 0; i < m_brainComponents.Count; ++i)
		{
			m_brainComponents[i].OnStart();
		}

		AIBrainManager brainManager = AIBrainManager.Instance as AIBrainManager;
		Debug.Assert(brainManager != null, "[AIBrain::Start] AIBrainManager instance is null");
		if (brainManager != null)
		{
			brainManager.RegisterAIBrain(this);
		}
	}

	// --------------------------------------------------------------------------------

	public virtual void Update()
	{
		for (int i = 0; i < m_brainComponents.Count; ++i)
		{
			m_brainComponents[i].OnUpdate();
		}
	}

	// -------------------------------------------------------------------------------/

	public virtual void DistributedUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDestroy()
	{
		AIBrainManager brainManager = AIBrainManager.Instance as AIBrainManager;
		if (brainManager != null)
		{
			brainManager.UnregisterAIBrain(this);
		}
	}

}
