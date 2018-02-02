using UnityEngine;

public class PerceptionTrigger : IPooledObject
{

	private static ObjectPool<PerceptionTrigger> m_perceptionTriggerPool = new ObjectPool<PerceptionTrigger>(4, new PerceptionTrigger());
	public static ObjectPool<PerceptionTrigger> Pool { get { return m_perceptionTriggerPool; } }

	// --------------------------------------------------------------------------------

	private PerceptionType m_perceptionType = PerceptionType.Hearing;
	public PerceptionType Perception
	{
		get { return m_perceptionType; }
		set { m_perceptionType = value; }
	}

	private PercievedAction m_percievedAction = PercievedAction.None;
	public PercievedAction Action
	{
		get { return m_percievedAction; }
		set { m_percievedAction = value; }
	}

	private Vector3 m_location = Vector3.zero;
	public Vector3 Location
	{
		get { return m_location; }
		set { m_location = value; }
	}

	private float m_range = 0.0f;
	public float Range
	{
		get { return m_range; }
		set { m_range = value; }
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

	public void Reset()
	{
		m_perceptionType = PerceptionType.Hearing;
		m_percievedAction = PercievedAction.None;
		m_location.Set(0.0f, 0.0f, 0.0f);
		m_range = 0.0f;
		m_actor = null;
		m_target = null;
	}
	
}
