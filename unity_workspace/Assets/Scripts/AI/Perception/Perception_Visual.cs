using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Visual : Perception
{

	[SerializeField]
	private float m_fieldOfView = 60.0f;

	[SerializeField]
	private float m_visionRange = 100.0f;

	// --------------------------------------------------------------------------------

	private RaycastHit m_raycastHit;

	// --------------------------------------------------------------------------------

	public override Sense Sense { get { return Sense.Visual; } }

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(SensoryTrigger trigger)
	{
		// #SteveD >>> return false if we're out of view range

		// #SteveD >>> return false if we're out of field of view

		if (trigger.Agent != null)
		{
			// #SteveD >>> cast a ray from owner eye location in direction of trigger.Agent
		}

		// #SteveD >>> should we cast a ray from 

		return false;
	}

	// --------------------------------------------------------------------------------

	// #SteveD >>> Draw ranged field of view

}
