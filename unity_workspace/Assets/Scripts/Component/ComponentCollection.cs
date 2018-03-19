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
			if (HasComponentOfType(components[i].GetType()))
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

	public bool HasComponentOfType(Type type)
	{
		return m_components.ContainsKey(type);
	}

	// --------------------------------------------------------------------------------
	
	public T GetComponentOfType<T>() where T : BaseComponent
	{
		BaseComponent baseComponent = null;
		if (m_components.TryGetValue(typeof(T), out baseComponent) && baseComponent != null)
		{
			return baseComponent as T;
		}
		return null;
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