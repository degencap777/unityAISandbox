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
			if (m_actor != null)
			{
				percievedEvent.Location = m_actor.Position;
			}
			percievedEvent.EventType = m_percievedEventType;
			percievedEvent.Target = m_target;
			if (m_actor == null && m_target != null)
			{
				percievedEvent.Location = m_target.Position;
			}

			PerceptionTrigger trigger = PerceptionTrigger.Pool.Get();
			trigger.PerceptionType = m_perceptionType;

			//trigger.Range = m_range;
			// #SteveD	>>> This needs removing or replacing..

			trigger.PerceptionEvent = percievedEvent;
			PerceptionTriggerDistributor.Instance.DistributeTrigger(trigger);
		}
	}

}
