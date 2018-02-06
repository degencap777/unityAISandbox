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
	// #SteveD	>>> implement serializable dictionary and use it here -> m_eventLifetimes
	private Dictionary<PerceptionEventType, float> m_eventLifetimes = new Dictionary<PerceptionEventType, float>();

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
		PerceptionEvent rememberedEvent = percievedEvent.Clone();
		if (rememberedEvent != null)
		{
			PerishablePerceptionEvent perishableEvent = PerishablePerceptionEvent.Pool.Get();
			if (perishableEvent != null)
			{
				perishableEvent.m_perceptionEvent = rememberedEvent;

				float lifetime = 1.0f;
				if (m_eventLifetimes.TryGetValue(rememberedEvent.Type, out lifetime) == false)
				{
					// #SteveD	>>> implement m_defaultEventLifetime
					lifetime = m_defaultEventLifetime;
				}
				perishableEvent.m_expirationTime = Time.deltaTime + lifetime;

				// #SteveD >>>	add perishable event to dictionary
			}
		}

		

		// #SteveD >>> process cloned event. If an existing event exists (same event, same agent, etc.) 
		// then just reset the expiration timer. otherwise grab a new perishablePerceptionEvent from the 
		// pool and add it to our memories
		// 
	}

	// --------------------------------------------------------------------------------

	public void RemoveExpiredMemories()
	{

		// #SteveD	>>> remove all memories that have expired

	}

}
