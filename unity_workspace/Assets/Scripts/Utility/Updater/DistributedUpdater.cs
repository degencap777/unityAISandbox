using System.Collections.Generic;
using UnityEngine;

public class DistributedUpdater<T> : MonoBehaviour where T : IDistributedUpdatable
{

	[SerializeField]
	private float m_updateInterval = 0.5f;
	
	// --------------------------------------------------------------------------------

	private List<T> m_updatables = new List<T>();
	private float m_previousElapsed = 0.0f;

	// --------------------------------------------------------------------------------
	
	public virtual void OnUpdate()
	{
		float currentElapsed = m_previousElapsed + Time.deltaTime;
		int startIndex = m_updateInterval > float.Epsilon ? CalculateIndexForTime(m_previousElapsed) : 0;
		int endIndex = m_updateInterval > float.Epsilon ? CalculateIndexForTime(currentElapsed) : m_updatables.Count;

		// update startIndex (inclusive) to endIndex (exclusive)
		for (int i = startIndex; i < Mathf.Min(endIndex, m_updatables.Count); ++i)
		{
			m_updatables[i].OnDistributedUpdate();
		}
		// handle endIndex overflowing
		if (endIndex >= m_updatables.Count)
		{
			for (int j = 0; j < Mathf.Min(endIndex - m_updatables.Count, startIndex); ++j)
			{
				m_updatables[j].OnDistributedUpdate();
			}
		}

		m_previousElapsed = currentElapsed;
		if (m_previousElapsed >= m_updateInterval)
		{
			m_previousElapsed %= m_updateInterval;
		}
	}
	
	// --------------------------------------------------------------------------------

	private int CalculateIndexForTime(float elapsed)
	{
		float normalised = (1.0f / m_updateInterval) * elapsed;
		return (int)(m_updatables.Count * normalised);
	}

	// --------------------------------------------------------------------------------

	public void RegisterUpdatable(T updatable)
	{
		m_updatables.Add(updatable);
	}

	// --------------------------------------------------------------------------------

	public void UnregisterUpdatable(T updatable)
	{
		m_updatables.Remove(updatable);
	}

}
