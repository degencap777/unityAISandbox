using System;

[Flags]
public enum PerceptionType : uint
{
	None = 0,
	Vision = (1 << 0),
	Hearing = (1 << 1),
}