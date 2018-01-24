using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Audible : Perception
{

	public override Sense Sense { get { return Sense.Audible; } }

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(SensoryTrigger trigger)
	{

		// #SteveD >>> check range, obstacles..

		return false;
	}

}
