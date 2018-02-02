using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Hearing : Perception
{

	public override PerceptionType PerceptionType { get { return PerceptionType.Hearing; } }

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(PercievedEvent percievedEvent)
	{
		if (percievedEvent == null)
		{
			return false;
		}

		float rangeSquared = percievedEvent.Range * percievedEvent.Range;
		float distanceSquared = (Owner.Transform.position - percievedEvent.Location).sqrMagnitude;
		return distanceSquared <= rangeSquared;
	}

}
