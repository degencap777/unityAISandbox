using System;
using System.Collections.Generic;
using UnityEngine;

public class DistributedUpdater<T> : MonoBehaviour where T : IDistributedUpdatable
{

	[Serializable]
	public class UpdatableBucket
	{
		public List<T> m_updatables = new List<T>();
		public float m_updateTime = 0.0f;
	}

	// --------------------------------------------------------------------------------

	[SerializeField, Range(0.0f, 10.0f)]
	private float m_updateInterval = 0.5f;

	[SerializeField, Range(1, 60)]
	private int m_targetBucketCount = 10;

	// --------------------------------------------------------------------------------

	private List<UpdatableBucket> m_updatableBuckets = new List<UpdatableBucket>();

	private float m_currentInterval = 0.0f;
	private int m_nextAddIndex = 0;

	// --------------------------------------------------------------------------------
	
	public virtual void OnUpdate()
	{
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
				//Debug.LogFormat("[DistributedUpdater] updating bucket {0}, update time: {1}, start interval: {2}, end interval: {3}",
				//	i, m_updatableBuckets[i].m_updateTime, m_currentInterval, endInterval);

				for (int j = 0; j < m_updatableBuckets[i].m_updatables.Count; ++j)
				{				
					m_updatableBuckets[i].m_updatables[j].DistributedUpdate();
				}
			}
		}

		m_currentInterval = endInterval;
		if (m_currentInterval >= m_updateInterval)
		{
			m_currentInterval -= endInterval;
		}
	}

	// --------------------------------------------------------------------------------

	public virtual void RegisterUpdatable(T updatable)
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

		//LogState();
	}

	// --------------------------------------------------------------------------------

	public virtual void UnregisterUpdatable(T updatable)
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

		//LogState();
	}

	// --------------------------------------------------------------------------------

	private void LogState()
	{
		Debug.LogFormat("[DistributedUpdater] update interval: {0}, buckets: {1}\n", 
			m_updateInterval, m_updatableBuckets.Count);
		
		for (int i = 0; i < m_updatableBuckets.Count; ++i)
		{
			Debug.LogFormat(" >>> index: {0}, update time: {1}, bucket items: {2}\n", 
				i, m_updatableBuckets[i].m_updateTime, m_updatableBuckets[i].m_updatables.Count);
		}
	}

	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public List<UpdatableBucket> Editor_UpdatableBuckets { get { return m_updatableBuckets; } }

#endif // UNITY_EDITOR

}
