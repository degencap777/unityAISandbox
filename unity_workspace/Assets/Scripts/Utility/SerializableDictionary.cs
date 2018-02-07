using System.Collections.Generic;
using UnityEngine;

public abstract class SerializableDictionary<K, V> : ISerializationCallbackReceiver
{

	[SerializeField]
	private List<K> m_keys = new List<K>();

	[SerializeField]
	private List<V> m_values = new List<V>();

	// --------------------------------------------------------------------------------

	private Dictionary<K, V> m_dictionary = new Dictionary<K, V>();

	// --------------------------------------------------------------------------------

	public void OnBeforeSerialize()
	{
		m_keys.Clear();
		m_values.Clear();

		foreach (var kvp in m_dictionary)
		{
			m_keys.Add(kvp.Key);
			m_values.Add(kvp.Value);
		}
	}

	// --------------------------------------------------------------------------------

	public void OnAfterDeserialize()
	{
		m_dictionary.Clear();
		for (int i = 0; i < m_keys.Count; ++i)
		{
			if (m_dictionary.ContainsKey(m_keys[i]) == false)
			{
				m_dictionary.Add(m_keys[i], m_values[i]);
			}
		}
	}

	// --------------------------------------------------------------------------------

	public bool TryGetValue(K key, out V value)
	{
		return m_dictionary.TryGetValue(key, out value);
	}

}