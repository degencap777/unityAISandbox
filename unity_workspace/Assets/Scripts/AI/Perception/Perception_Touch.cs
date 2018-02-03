using UnityEngine;

[DisallowMultipleComponent]
public class Perception_Touch : Perception
{

	public override PerceptionType PerceptionType { get { return PerceptionType.Touch; } }
	
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
		return false;
	}

}