using UnityEngine;

public class AgentBlackboardComponent : BaseComponent
{
	
	[SerializeField]
	private AgentBlackboardConfig m_config = null;

	// --------------------------------------------------------------------------------

	private SharedBlackboard m_sharedBlackboard = null;
	public SharedBlackboard SharedBlackboard { get { return m_sharedBlackboard; } }

	private ComponentCollection m_componentCollection = null;
	public ComponentCollection ComponentCollection { get { return m_componentCollection; } }

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<AgentBlackboardConfig>();
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		// #SteveD	>>> requires reference to shared blackboard
		//m_sharedBlackboard = ...;
		//Debug.Assert(m_sharedBlackboard != null, "[AgentBlackboardComponent::OnStart] SharedBlackboard is null\n");
		// ------------
		// Or should the blackboard be aware of all Agents?
		// <<<<<<<<<<<<

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

	public AgentBlackboardConfig Editor_Config { get { return m_config; } }

#endif // UNITY_EDITOR

}