using UnityEngine;

public abstract class Perception : AIBrainComponent
{

	public abstract PerceptionType PerceptionType { get; }

	// --------------------------------------------------------------------------------

	protected abstract bool CanPercieve(PercievedEvent percievedEvent);

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

		if (CanPercieve(trigger.PercievedEvent))
		{
			// #SteveD >>> process trigger.PercievedEvent >>> add to memories (working, historic)
			return true;
		}

		return false;
	}

}
