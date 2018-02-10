using UnityEngine;

public abstract class Perception : AIBrainComponent
{

	protected Transform m_transform = null;

	// --------------------------------------------------------------------------------

	public abstract PerceptionType PerceptionType { get; }

	// --------------------------------------------------------------------------------

	protected abstract bool CanPercieve(PerceptionEvent percievedEvent);

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		m_transform = GetComponent<Transform>();
		Debug.Assert(m_transform != null, "[Perception::OnAwake] GetComponent<Transform> failed\n");
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		PerceptionTriggerDistributor perceptionManager = PerceptionTriggerDistributor.Instance;
		if (perceptionManager != null)
		{
			perceptionManager.RegisterPerception(this);
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDestroy()
	{
		PerceptionTriggerDistributor perceptionManager = PerceptionTriggerDistributor.Instance;
		if (perceptionManager != null)
		{
			perceptionManager.UnregisterPerception(this);
		}
	}

	// --------------------------------------------------------------------------------

	public virtual bool Percieve(PerceptionTrigger trigger)
	{
		if (trigger.PerceptionType != PerceptionType)
		{
			return false;
		}

		if (m_transform == null)
		{
			return false;
		}

		// validate perception event
		PerceptionEvent percievedEvent = trigger.PerceptionEvent;
		if (percievedEvent == null)
		{
			return false;
		}

		// position of interest
		Vector3 eventPosition = percievedEvent.Actor == null ?
			percievedEvent.Location :
			percievedEvent.Actor.Transform.position;

		Vector3 toEvent = eventPosition - m_transform.position;
		if (toEvent.sqrMagnitude > trigger.Range * trigger.Range)
		{
			return false;
		}

		if (CanPercieve(trigger.PerceptionEvent))
		{
			if (m_brain != null)
			{
				if (m_brain.HistoricMemory != null)
				{
					m_brain.HistoricMemory.ProcessPerceptionEvent(trigger.PerceptionEvent);
				}
				
				if (m_brain.WorkingMemory != null)
				{
					m_brain.WorkingMemory.ProcessPerceptionEvent(trigger.PerceptionEvent);
				}
			}
			return true;
		}

		return false;
	}

}
