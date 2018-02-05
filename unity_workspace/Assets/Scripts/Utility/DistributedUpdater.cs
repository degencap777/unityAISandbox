using System.Collections.Generic;
using UnityEngine;

public abstract class DistributedUpdater<T> : MonoBehaviour where T : IDistributedUpdatable
{

	private class UpdatableBucket
	{

		public List<T> m_updatables = new List<T>();
		public float m_updateTime = 0.0f;

	}

	// --------------------------------------------------------------------------------

	//[SerializeField, Range(0.0f, 1.0f)]
	[SerializeField, Range(0.0f, 10.0f)] // #SteveD >>> for testing
	private float m_updateInterval = 0.5f;

	[SerializeField, Range(1, 60)]
	private int m_targetBucketCount = 30;

	// --------------------------------------------------------------------------------

	private List<UpdatableBucket> m_updatableBuckets = new List<UpdatableBucket>();

	private float m_currentInterval = 0.0f;
	private int m_nextAddIndex = 0;

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		// #SteveD	>>> requires an AIBrainManager to hold onto the DistributedUpdater<AIBrain> object
		// #SteveD	>>> test this with m_updateInterval @ 9.0f, m_targetBucketCount @ 6 and 13 agents
		//			>>> add a bunch of logging to Register and Update

		if (m_updatableBuckets.Count == 0)
		{
			return;
		}
		
		float endInterval = m_currentInterval + Time.deltaTime;
		for (int i = 0; i < m_updatableBuckets.Count; ++i)
		{
			float updateTime = m_updatableBuckets[i].m_updateTime;

			if ((updateTime >= m_currentInterval && updateTime < endInterval) || 
				((updateTime - m_updateInterval) >= m_currentInterval && (updateTime - m_updateInterval) < endInterval))
			{
				for (int j = 0; j < m_updatableBuckets[i].m_updatables.Count; ++j)
				{
					m_updatableBuckets[i].m_updatables[j].DistributedUpdate();
				}
			}
		}

		m_currentInterval = endInterval;
	}

	// --------------------------------------------------------------------------------

	public void RegisterUpdatable(T updatable)
	{
		// check for reaching last bucket
		if (m_nextAddIndex >= m_updatableBuckets.Count)
		{
			if (m_updatableBuckets.Count == m_targetBucketCount)
			{
				// max buckets reached, loop around to 0
				m_nextAddIndex = 0;
			}
			else
			{
				// add bucket
				m_updatableBuckets.Add(new UpdatableBucket());
				RecalculateBucketUpdateTimes();
			}
		}

		// add
		m_updatableBuckets[m_nextAddIndex].m_updatables.Add(updatable);
		++m_nextAddIndex;
	}

	// --------------------------------------------------------------------------------

	public void UnregisterUpdatable(T updatable)
	{
		for (int i = 0; i < m_updatableBuckets.Count; ++i)
		{
			m_updatableBuckets[i].m_updatables.Remove(updatable);

			// handle empty bucket
			if (m_updatableBuckets[i].m_updatables.Count == 0)
			{
				m_updatableBuckets.RemoveAt(i);
				RecalculateBucketUpdateTimes();
				break;
			}
		}
	}

	// --------------------------------------------------------------------------------

	private void RecalculateBucketUpdateTimes()
	{
		if (m_updatableBuckets.Count == 0)
		{
			return;
		}

		float interval = m_updateInterval / m_updatableBuckets.Count;
		for (int i = 0; i < m_updatableBuckets.Count; ++i)
		{
			m_updatableBuckets[i].m_updateTime = interval * i;
		}
	}

}
