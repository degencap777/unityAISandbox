using UnityEngine;

public abstract class Perception : MonoBehaviour
{

	public abstract Sense Sense { get; }

	// --------------------------------------------------------------------------------

	public abstract bool Percieve(SensoryTrigger trigger);
	
}
