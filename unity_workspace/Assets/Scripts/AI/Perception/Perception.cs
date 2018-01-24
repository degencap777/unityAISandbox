using UnityEngine;

public abstract class Perception : AIBrainComponent
{

	public abstract Sense Sense { get; }

	// --------------------------------------------------------------------------------

	protected abstract bool CanPercieve(SensoryTrigger trigger);

	// --------------------------------------------------------------------------------

	public virtual bool Percieve(SensoryTrigger trigger)
	{
		if (trigger.Sense != Sense)
		{
			return false;
		}

		if (CanPercieve(trigger))
		{
			return false;
		}

		return true;
	}

}
