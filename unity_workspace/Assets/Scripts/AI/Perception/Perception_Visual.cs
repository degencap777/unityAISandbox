using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Visual : Perception
{
	
	[SerializeField]
	private float m_horizontalFieldOfView = 65.0f;

	[SerializeField]
	private float m_verticalFieldOfView = 15.0f;

	[SerializeField, Range(1.0f, 180.0f)]
	private float m_visionRange = 10.0f;
	private float m_visionRangeSquared = 100.0f;

	private Transform m_transform = null;

	// --------------------------------------------------------------------------------

	public override PerceptionType PerceptionType { get { return PerceptionType.Visual; } }

	// --------------------------------------------------------------------------------

	public virtual void OnValidate()
	{
		CalculateVisionRangeSquared();
	}

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		base.OnAwake();

		m_transform = GetComponent<Transform>();
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		CalculateVisionRangeSquared();
	}

	// --------------------------------------------------------------------------------

	private void CalculateVisionRangeSquared()
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
		// validate
		if (trigger == null || m_transform == false)
		{
			return false;
		}

		// points of interest
		Vector3 eyePosition = m_transform.position;
		Vector3 eventPosition = trigger.Actor == null ? 
			trigger.Location : 
			trigger.Actor.Transform.position;

		// vector to event
		Vector3 toEvent = eventPosition - eyePosition;
		// distance squared to event
		float toEventDistanceSquared = toEvent.sqrMagnitude;

		// fail if out of vision range
		if (toEventDistanceSquared > m_visionRangeSquared)
		{
			return false;
		}

		// #SteveD >>> if is beyond m_visionRange,
		//	return false

		// #SteveD >>> if is out of horizontal fov,
		//	return false

		// #SteveD >>> if is out of vertical fov,
		//	return false

		// #SteveD >>> raycast to target
		//		>>> exclude target
		//		>>> exclude owner (always)
		//		>>> if we have 0 collisions, we have a clear path to the target event/object

		return false;
	}

	public bool CanSee(Vector3 position, GameObject excludeFromRaycast = null)
	{
		// refactor code in CanPercieve into here

		return false;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		// #SteveD >>> represent view cone
	}

}
