using UnityEngine;

public class AgentBlackboardComponent : BaseComponent
{
	
	[SerializeField]
	private AgentBlackboardSettings m_settings = null;

	// --------------------------------------------------------------------------------

	private SharedBlackboard m_sharedBlackboard = null;
	public SharedBlackboard SharedBlackboard { get { return m_sharedBlackboard; } }

	private ComponentCollection m_componentCollection = null;
	public ComponentCollection ComponentCollection { get { return m_componentCollection; } }

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_settings == null)
		{
			m_settings = ScriptableObject.CreateInstance<AgentBlackboardSettings>();
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		m_sharedBlackboard = SharedBlackboard.Instance;
		Debug.Assert(m_sharedBlackboard != null, "[AgentBlackboardComponent::OnStart] SharedBlackboard is null\n");

		m_componentCollection = GetComponentInParent<ComponentCollection>();
		Debug.Assert(m_componentCollection != null, "[AgentBlackboardComponent::OnStart] GetComponentInParent<ComponentCollection>() failed\n");
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void Destroy()
	{
		;
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public AgentBlackboardSettings Editor_Settings { get { return m_settings; } }

#endif // UNITY_EDITOR

}