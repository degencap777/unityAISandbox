using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Visual : Perception
{

	[SerializeField]
	private float m_fieldOfView = 60.0f;

	[SerializeField]
	private float m_visionRange = 10.0f;
	private float m_visionRangeSquared = 100.0f;

	// --------------------------------------------------------------------------------

	private RaycastHit m_raycastHit;

	// --------------------------------------------------------------------------------

	public override PerceptionType PerceptionType { get { return PerceptionType.Visual; } }

	// --------------------------------------------------------------------------------

	protected virtual void OnValidate()
	{
		m_visionRangeSquared = m_visionRange * m_visionRange;
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		m_visionRangeSquared = m_visionRange * m_visionRange;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(PerceptionTrigger trigger)
	{
		if (Owner == null || trigger == null || trigger.Actor == null)
		{
			return false;
		}

		// check if we're in range of the trigger
		float rangeSquared = Mathf.Max(m_visionRangeSquared, trigger.Range * trigger.Range);
		float distanceSquared = (Owner.Transform.position - trigger.Location).sqrMagnitude;
		if (distanceSquared > rangeSquared)
		{
			return false;
		}

		// check if the trigger location is within our field of view
		Vector3 toLocation = trigger.Location - Owner.Transform.position;
		if (Vector3.Angle(toLocation, Owner.Transform.forward) > m_fieldOfView * 0.5f)
		{
			return false;
		}

		// #SteveD >>> cast a ray from owner eye location in direction of trigger.Agent
		//	>>> requires eye location setting up on Agent

		return true;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		if (Owner != null)
		{
			Color cachedColour = Gizmos.color;

			// #SteveD >>> Draw ranged view cone or lines & curve

			//Gizmos.color = Color.red;
			//Gizmos.DrawSphere(Owner.Transform.position, m_visionRange);

			Gizmos.color = cachedColour;
		}
	}

}
