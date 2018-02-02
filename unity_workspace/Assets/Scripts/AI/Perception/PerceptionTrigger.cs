using UnityEngine;

public class PerceptionTrigger : IPooledObject
{

	private static ObjectPool<PerceptionTrigger> m_perceptionTriggerPool = new ObjectPool<PerceptionTrigger>(4, new PerceptionTrigger());
	public static ObjectPool<PerceptionTrigger> Pool { get { return m_perceptionTriggerPool; } }

	// --------------------------------------------------------------------------------

	private PerceptionType m_perceptionType = PerceptionType.Hearing;
	public PerceptionType PerceptionType
	{
		get { return m_perceptionType; }
		set { m_perceptionType = value; }
	}

	private PercievedEvent m_percievedEvent = null;
	public PercievedEvent PercievedEvent
	{
		get { return m_percievedEvent; }
		set { m_percievedEvent = value; }
	}

	// --------------------------------------------------------------------------------

	public void ReleaseResources()
	{
		PercievedEvent.Pool.Return(m_percievedEvent);
	}

	// --------------------------------------------------------------------------------

	public void Reset()
	{
		m_perceptionType = PerceptionType.Hearing;
		m_percievedEvent = null;
	}
	
}
