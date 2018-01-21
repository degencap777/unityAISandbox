using UnityEngine;

public class SensoryTrigger : IPooledObject
{

	private Sense m_sense = Sense.Audible;
	public Sense Sense { get { return m_sense; } }

	private SensedAction m_sensedAction = SensedAction.None;
	public SensedAction SensedAction { get { return m_sensedAction; } }

	private Vector3 m_location = Vector3.zero;
	public Vector3 Location { get { return m_location; } }

	private float m_range = 0.0f;
	public float Range { get { return m_range; } }

	private Agent m_actor = null;
	public Agent Actor { get { return m_actor; } }

	private Agent m_mark = null;
	public Agent Mark { get { return m_mark; } }

	// --------------------------------------------------------------------------------

	// #SteveD >>> set values (once grabbed from pool)

	// --------------------------------------------------------------------------------

	public void ReleaseResources()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void Reset()
	{
		m_sense = Sense.Audible;
		m_sensedAction = SensedAction.None;
		m_location.Set(0.0f, 0.0f, 0.0f);
		m_range = 0.0f;
		m_actor = null;
		m_mark = null;
	}
	
}
