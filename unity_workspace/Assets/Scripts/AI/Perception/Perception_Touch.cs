

public class Perception_Touch : Perception
{

	// #SteveD >>> implement touch perception

	public override PerceptionType PerceptionType { get { return PerceptionType.Touch; } }

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

	protected override bool CanPercieve(PerceptionTrigger trigger)
	{
		return false;
	}

}