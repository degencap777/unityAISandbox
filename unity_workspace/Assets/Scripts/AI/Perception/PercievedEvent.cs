using UnityEngine;
using System.Collections;

public class PercievedEvent : IPooledObject
{

	private static ObjectPool<PercievedEvent> m_percievedEventPool = new ObjectPool<PercievedEvent>(4, new PercievedEvent());
	public static ObjectPool<PercievedEvent> Pool { get { return m_percievedEventPool; } }

	// --------------------------------------------------------------------------------
	
	private PercievedEventType m_percievedEventType = PercievedEventType.None;
	public PercievedEventType Action
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

	private float m_range = 0.0f;
	public float Range
	{
		get { return m_range; }
		set { m_range = value; }
	}

	// --------------------------------------------------------------------------------

	public void ReleaseResources()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void Reset()
	{
		m_percievedEventType = PercievedEventType.None;
		m_location.Set(0.0f, 0.0f, 0.0f);
		m_actor = null;
		m_target = null;
		m_range = 0.0f;
	}
	
}
