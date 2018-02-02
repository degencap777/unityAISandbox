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

	public override PerceptionType PerceptionType { get { return PerceptionType.Vision; } }

	// --------------------------------------------------------------------------------

	public virtual void OnValidate()
	{
		CalculateVisionRangeSquared();
	}

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		m_transform = GetComponent<Transform>();
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		base.OnStart();

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

		// fail if out of horizontal FOV
		Vector3 toEventHorizontal = toEvent;
		toEventHorizontal.y = 0.0f;
		if (Vector3.Angle(m_transform.forward, toEventHorizontal) > m_horizontalFieldOfView * 0.5f)
		{
			return false;
		}

		// fail if out of vertical FOV
		Vector3 toEventVertical = toEvent;
		toEventVertical.x = 0.0f;
		toEventVertical.z = 0.0f;
		if (Vector3.Angle(m_transform.forward, toEventVertical) > m_verticalFieldOfView * 0.5f)
		{
			return false;
		}
		
		// #SteveD >>> raycast to target
		//		>>> exclude target
		//		>>> exclude owner (always)
		//		>>> if we have 0 collisions, we have a clear path to the target event/object

		return false;
	}
	
	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		// #SteveD >>> represent view cone
	}

}
