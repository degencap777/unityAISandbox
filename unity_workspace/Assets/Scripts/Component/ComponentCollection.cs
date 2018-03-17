using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentCollection : MonoBehaviour
{

	private Dictionary<Type, BaseComponent> m_components = new Dictionary<Type, BaseComponent>();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		var components = GetComponentsInChildren<BaseComponent>();
		for (int i = 0; i < components.Length; ++i)
		{
			if (HasComponent(components[i].GetType()))
			{
				this.LogError(string.Format("Attempting to add multiple components of the same type: {0}", components[i].GetType().ToString()));
			}
			else
			{
				m_components.Add(components[i].GetType(), components[i]);
			}
		}

		var componentEnumerator = m_components.Values.GetEnumerator();
		while (componentEnumerator.MoveNext())
		{
			componentEnumerator.Current.OnAwake();
		}
	}

	// --------------------------------------------------------------------------------

	public virtual void Start()
	{
		var componentEnumerator = m_components.Values.GetEnumerator();
		while (componentEnumerator.MoveNext())
		{
			componentEnumerator.Current.OnStart();
		}
	}

	// --------------------------------------------------------------------------------

	public bool HasComponent(Type componentType)
	{
		return m_components.ContainsKey(componentType);
	}

	// --------------------------------------------------------------------------------
	
	public bool GetComponent<T>(out BaseComponent component)
	{
		return m_components.TryGetValue(typeof(T), out component);
	}

	// --------------------------------------------------------------------------------

	public virtual void OnUpdate()
	{
		var componentEnumerator = m_components.Values.GetEnumerator();
		while (componentEnumerator.MoveNext())
		{
			componentEnumerator.Current.OnUpdate();
		}
	}

}