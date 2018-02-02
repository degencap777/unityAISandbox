using UnityEngine;

public class PerceptionTest : Test
{

	[SerializeField]
	private Agent m_actor = null;

	[SerializeField]
	private PerceptionType m_perceptionType = PerceptionType.Vision;

	[SerializeField]
	private PercievedEventType m_percievedEventType = PercievedEventType.None;

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
			PercievedEvent percievedEvent = PercievedEvent.Pool.Get();
			percievedEvent.Actor = m_actor;
			percievedEvent.Action = m_percievedEventType;
			percievedEvent.Target = m_target;
			percievedEvent.Range = m_range;

			PerceptionTrigger trigger = PerceptionTrigger.Pool.Get();
			trigger.PerceptionType = m_perceptionType;
			trigger.PercievedEvent = percievedEvent;
			PerceptionTriggerDistributor.Instance.DistributeTrigger(trigger);
		}
	}

}
