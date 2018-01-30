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

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(PerceptionTrigger trigger)
	{
		float rangeSquared = trigger.Range * trigger.Range;
		float distanceSquared = (Owner.Transform.position - trigger.Location).sqrMagnitude;
		return distanceSquared <= rangeSquared;
	}

}
