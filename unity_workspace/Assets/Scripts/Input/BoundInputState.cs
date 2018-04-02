using System;

namespace AISandbox.Input
{
	[Serializable]
	public abstract class BoundInputState
	{

		public abstract void Update();
		public abstract bool ConditionsMet();
		public abstract float GetValue();

	}
}