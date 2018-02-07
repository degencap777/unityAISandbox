using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HistoricMemory : AIBrainComponent, IAIMemory
{

	private class PerishablePerceptionEvent : IPooledObject
	{

		private static ObjectPool<PerishablePerceptionEvent> m_pool =
			new ObjectPool<PerishablePerceptionEvent>(32, new PerishablePerceptionEvent());
		public static ObjectPool<PerishablePerceptionEvent> Pool { get { return m_pool; } }

		// ----------------------------------------------------------------------------

		public PerceptionEvent m_perceptionEvent = null;
		public float m_expirationTime = -1.0f;

		// ----------------------------------------------------------------------------

		public void ReleaseResources()
		{
			;
		}

		// ----------------------------------------------------------------------------

		public void Reset()
		{
			m_perceptionEvent = null;
			m_expirationTime = -1.0f;
		}

	}

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_defaultMemoryLifetime = 1.0f;

	[SerializeField]
	private PerceptionEventTypeFloatDictionary m_eventLifetimes = new PerceptionEventTypeFloatDictionary();
	
	// --------------------------------------------------------------------------------

	private Dictionary<PerceptionEventType, List<PerishablePerceptionEvent>> m_rememberedEvents = 
		new Dictionary<PerceptionEventType, List<PerishablePerceptionEvent>>();
	
	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		;
	}
	
	// --------------------------------------------------------------------------------

	public void ProcessPerceptionEvent(PerceptionEvent percievedEvent)
	{
		// #SteveD	>>> check here if it already exists (event with same action, agent, etc.). If so, just update the existing event

		PerceptionEvent rememberedEvent = percievedEvent.Clone();
		if (rememberedEvent != null)
		{
			PerishablePerceptionEvent perishableEvent = PerishablePerceptionEvent.Pool.Get();
			if (perishableEvent != null)
			{
				perishableEvent.m_perceptionEvent = rememberedEvent;

				float lifetime = m_defaultMemoryLifetime;
				if (m_eventLifetimes.TryGetValue(rememberedEvent.Action, out lifetime) == false)
				{
					lifetime = m_defaultMemoryLifetime;
				}
				perishableEvent.m_expirationTime = Time.deltaTime + lifetime;

				// #SteveD >>>	add perishable event to list in dictionary, or add new list if one doesn't already exist
			}
		}
	}

	// --------------------------------------------------------------------------------

	public void RemoveExpiredMemories()
	{

		// #SteveD	>>> remove all expired memories

	}

}
