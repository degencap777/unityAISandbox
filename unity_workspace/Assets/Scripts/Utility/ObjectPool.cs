using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
	void ReleaseResources();
	void Reset();
}

// ------------------------------------------------------------------------------------

public class ObjectPool<T> where T : IPooledObject
{

	private static readonly uint k_minInitialCapacity = 2;
	private static readonly uint k_maxInitialCapacity = 128;

	// --------------------------------------------------------------------------------

	private List<T> m_pool = new List<T>();
	
	// --------------------------------------------------------------------------------

	public ObjectPool(uint poolSize, T prototype)
	{
		if (poolSize < k_minInitialCapacity)
		{
			poolSize = k_minInitialCapacity;
		}
		else if (poolSize > k_maxInitialCapacity)
		{
			poolSize = k_maxInitialCapacity;
		}
		IncreasePoolSize(poolSize);
	}

	// --------------------------------------------------------------------------------

	private void IncreasePoolSize(uint objectCount)
	{
		for (uint i = 0; i < objectCount; ++i)
		{
			m_pool.Add(Activator.CreateInstance<T>());
		}
	}

	// --------------------------------------------------------------------------------

	public T Get()
	{
		if (m_pool.Count == 0)
		{
			// double in size
			IncreasePoolSize((uint)m_pool.Capacity);
		}

		if (m_pool.Count > 0)
		{
			T obj = m_pool[m_pool.Count - 1];
			m_pool.RemoveAt(m_pool.Count - 1);
			return obj;
		}

		Debug.LogErrorFormat("[ObjectPool<{0}>] Empty pool, unable to allocate an object\n", typeof(T));
		return default(T);
	}

	// --------------------------------------------------------------------------------

	public void Return(T obj)
	{
		if (obj != null)
		{
			obj.ReleaseResources();
			obj.Reset();
			m_pool.Add(obj);
		}
	}

}
