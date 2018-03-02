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
			percievedEvent.Actor.Position;

		// #SteveD	>>> requires a perception zone(s) (see GameAI Pro)
		//			>>> must be definable in editor
		//			>>> must be generic enough to represent different senses (sight, hearing, etc.)
		//			>>> should live on base perception class (this) and be tweakable
		//			>>> requires intuitive custom editor & gizmo
		//			>>> Requires owning class with multiple layers of zones with different values

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
