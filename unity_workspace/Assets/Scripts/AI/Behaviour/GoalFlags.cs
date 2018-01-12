using System;

[Flags]
public enum GoalFlags : uint
{
	None = 0,
	CloseDown = (1 << 0),
	Escape = (1 << 1),
}
