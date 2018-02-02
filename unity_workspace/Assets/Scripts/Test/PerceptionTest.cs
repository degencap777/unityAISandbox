using UnityEngine;

public class PerceptionTest : Test
{

	[SerializeField]
	private Agent m_actor = null;

	[SerializeField]
	private PerceptionType m_perceptionType = PerceptionType.Vision;

	[SerializeField]
	private PercievedAction m_percievedAction = PercievedAction.None;

	[SerializeField]
	private Agent m_target = null;

	[SerializeField]
	private float m_range = 1.0f;

	// --------------------------------------------------------------------------------

	protected override void ResetTests()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override void RunTests()
	{
		PerceptionTriggerDistributor distributor = PerceptionTriggerDistributor.Instance;
		if (distributor != null)
		{
			PerceptionTrigger trigger = PerceptionTrigger.Pool.Get();
			trigger.Actor = m_actor;
			trigger.Perception = m_perceptionType;
			trigger.Action = m_percievedAction;
			trigger.Target = m_target;
			trigger.Range = m_range;
			PerceptionTriggerDistributor.Instance.DistributeTrigger(trigger);
		}
	}

}
