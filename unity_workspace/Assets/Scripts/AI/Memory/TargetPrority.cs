using System;

public class TargetPriority : IComparable
{
	private Agent m_target = null;
	public Agent Target { get { return m_target; } }

	private float m_priority = 1.0f;
	public float Priority { get { return m_priority; } }

	// ----------------------------------------------------------------------------

	public TargetPriority(Agent target, float initialPriority = 0.0f)
	{
		m_target = target;
		m_priority = initialPriority;
	}

	// ----------------------------------------------------------------------------

	public void UpdatePriority(float newPriority)
	{
		m_priority = newPriority;
	}

	// ----------------------------------------------------------------------------

	public int CompareTo(object obj)
	{
		TargetPriority otherTargetPriority = obj as TargetPriority;
		if (otherTargetPriority != null)
		{
			return m_priority.CompareTo(otherTargetPriority.m_priority);
		}
		return -1;
	}

}