using UnityEngine;

public class TimeCache : ScriptableObject
{

	private float m_currentTime = 0.0f;
	public float CurrentTime { get { return m_currentTime; } }

	private float m_deltaTime = 0.0f;
	public float DeltaTime { get { return m_deltaTime; } }

	private int m_frame = 0;
	public int Frame { get { return m_frame; } }

	// --------------------------------------------------------------------------------

	protected virtual void OnEnable()
	{
		m_currentTime = 0.0f;
		m_deltaTime = 0.0f;
		m_frame = 0;
	}

}