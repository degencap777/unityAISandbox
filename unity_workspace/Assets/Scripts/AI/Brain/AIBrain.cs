using UnityEngine;

[DisallowMultipleComponent]
public class AIBrain : MonoBehaviour, IDistributedUpdatable
{

	private Agent m_owner = null;
	public Agent Owner { get { return m_owner; } }
	
	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_owner = GetComponentInParent<Agent>();
	}

	// --------------------------------------------------------------------------------

	protected virtual void Start()
	{
		AIBrainManager brainManager = AIBrainManager.Instance;
		Debug.Assert(brainManager != null, "[AIBrain::Start] AIBrainManager instance is null\n");

		if (brainManager != null)
		{
			brainManager.RegisterAIBrain(this);
		}
	}

	// --------------------------------------------------------------------------------

	public virtual void Update()
	{
		;
	}

	// --------------------------------------------------------------------------------

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
