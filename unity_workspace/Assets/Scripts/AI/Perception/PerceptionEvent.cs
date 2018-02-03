using UnityEngine;

public class PerceptionEvent : IPooledObject
{

	private static ObjectPool<PerceptionEvent> m_percievedEventPool = 
		new ObjectPool<PerceptionEvent>(32, new PerceptionEvent());
	public static ObjectPool<PerceptionEvent> Pool { get { return m_percievedEventPool; } }

	// --------------------------------------------------------------------------------
	
	private PerceptionEventType m_percievedEventType = PerceptionEventType.None;
	public PerceptionEventType Action
	{
		get { return m_percievedEventType; }
		set { m_percievedEventType = value; }
	}

	private Vector3 m_location = Vector3.zero;
	public Vector3 Location
	{
		get { return m_location; }
		set { m_location = value; }
	}

	private Agent m_actor = null;
	public Agent Actor
	{
		get { return m_actor; }
		set { m_actor = value; }
	}

	private Agent m_target = null;
	public Agent Target
	{
		get { return m_target; }
		set { m_target = value; }
	}
	
	// --------------------------------------------------------------------------------

	public void ReleaseResources()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public PerceptionEvent Clone()
	{
		PerceptionEvent clonedEvent = Pool.Get();
		if (clonedEvent != null)
		{
			clonedEvent.m_location = m_location;
			clonedEvent.m_actor = m_actor;
			clonedEvent.m_target = m_target;
		}
		return clonedEvent;
	}

	// --------------------------------------------------------------------------------

	public void Reset()
	{
		m_percievedEventType = PerceptionEventType.None;
		m_location.Set(0.0f, 0.0f, 0.0f);
		m_actor = null;
		m_target = null;
	}
	
}
