using UnityEngine;

public class PerceptionTest : Test
{

	[SerializeField]
	private Agent m_actor = null;

	[SerializeField]
	private PerceptionType m_perceptionType = PerceptionType.Vision;

	[SerializeField]
	private PerceptionEventType m_percievedEventType = PerceptionEventType.None;

	[SerializeField]
	private Agent m_target = null;

	[SerializeField]
	private float m_range = 10.0f;

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
			PerceptionEvent percievedEvent = PerceptionEvent.Pool.Get();
			percievedEvent.Actor = m_actor;
			percievedEvent.Action = m_percievedEventType;
			percievedEvent.Target = m_target;

			PerceptionTrigger trigger = PerceptionTrigger.Pool.Get();
			trigger.PerceptionType = m_perceptionType;
			trigger.Range = m_range;
			trigger.PerceptionEvent = percievedEvent;
			PerceptionTriggerDistributor.Instance.DistributeTrigger(trigger);
		}
	}

}
