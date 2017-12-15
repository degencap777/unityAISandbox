using System;

[Serializable]
public abstract class BoundInputState
{

	public abstract void Update();
	public abstract bool ConditionsMet();
	public abstract float GetValue();
	
}
