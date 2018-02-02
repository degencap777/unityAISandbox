

public class Perception_Smell : Perception
{

	// #SteveD >>> implement smell perception

	public override PerceptionType PerceptionType { get { return PerceptionType.Smell; } }

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
