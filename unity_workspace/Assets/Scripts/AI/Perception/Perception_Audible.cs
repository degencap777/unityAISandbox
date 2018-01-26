using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Audible : Perception
{

	public override PerceptionType PerceptionType { get { return PerceptionType.Audible; } }

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(PerceptionTrigger trigger)
	{
		// check if we're in range of the trigger
		float rangeSquared = trigger.Range * trigger.Range;
		float distanceSquared = (Owner.Transform.position - trigger.Location).sqrMagnitude;
		if (distanceSquared > rangeSquared)
		{
			return false;
		}

		// #SteveD >>> do we need to handle objects inbetween owner and trigger?
		//	>>> don't really want to employ further raycasts..

		return true;
	}

}
