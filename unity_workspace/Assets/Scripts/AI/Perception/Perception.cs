using UnityEngine;

public abstract class Perception : AIBrainComponent
{

	public abstract PerceptionType PerceptionType { get; }

	// --------------------------------------------------------------------------------

	protected abstract bool CanPercieve(PerceptionTrigger trigger);

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
		if (trigger.Perception != PerceptionType)
		{
			return false;
		}

		if (CanPercieve(trigger))
		{
			// #SteveD >>> process trigger >>> add to memories (working, historic)
			return true;
		}

		return false;
	}

}
