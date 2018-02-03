using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Hearing : Perception
{

	public override PerceptionType PerceptionType { get { return PerceptionType.Hearing; } }

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override bool CanPercieve(PerceptionEvent percievedEvent)
	{
		if (percievedEvent == null)
		{
			return false;
		}
		return true;
	}

}
