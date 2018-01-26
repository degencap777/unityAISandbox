using UnityEngine;

public abstract class Perception : AIBrainComponent
{

	public abstract PerceptionType PerceptionType { get; }

	// --------------------------------------------------------------------------------

	protected abstract bool CanPercieve(PerceptionTrigger trigger);
	
	// --------------------------------------------------------------------------------

	protected override void Awake()
	{
		base.Awake();

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
			// #SteveD >>> react to trigger
		}

		return false;
	}

}
