using UnityEngine;

public class AgentBlackboardComponent : BaseComponent
{

	private SharedBlackboard m_sharedBlackboard = null;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		m_sharedBlackboard = SharedBlackboard.Instance;
		Debug.Assert(m_sharedBlackboard != null, "[AgentBlackboardComponent::OnStart] SharedBlackboard is null\n");
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

}