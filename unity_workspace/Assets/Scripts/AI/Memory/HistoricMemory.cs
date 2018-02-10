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
	private PerceptionEventType_Float_Dictionary m_eventLifetimes = new PerceptionEventType_Float_Dictionary();

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
		if (percievedEvent == null)
		{
			return;
		}

		float lifetime = 0.0f;
		if (m_eventLifetimes.TryGetValue(percievedEvent.Action, out lifetime) == false)
		{
			lifetime = m_defaultMemoryLifetime;
		}
		float expiryTime = Time.time + lifetime;

		// check for this event already existing
		List<PerishablePerceptionEvent> eventList = null;
		if (m_rememberedEvents.TryGetValue(percievedEvent.Action, out eventList))
		{
			for (int i = 0; i < eventList.Count; ++i)
			{
				if (eventList[i].m_perceptionEvent.Action == percievedEvent.Action &&
					eventList[i].m_perceptionEvent.Actor == percievedEvent.Actor &&
					eventList[i].m_perceptionEvent.Target == percievedEvent.Target)
				{
					eventList[i].m_perceptionEvent.Location = percievedEvent.Location;
					if (expiryTime > eventList[i].m_expirationTime)
					{
						Debug.LogFormat("[HistoricMemory] Updated an existing {0} event expiry time from {1} to {2}\n",
							eventList[i].m_perceptionEvent.Action, eventList[i].m_expirationTime, expiryTime);
						eventList[i].m_expirationTime = expiryTime;
					}

					// updated event, return
					return;
				}
			}
		}

		// clone the perceived event
		PerceptionEvent rememberedEvent = percievedEvent.Clone();
		if (rememberedEvent != null)
		{
			// create a perishable perception event
			PerishablePerceptionEvent perishableEvent = PerishablePerceptionEvent.Pool.Get();
			if (perishableEvent != null)
			{
				// assign the cloned perception event to our new perishable event
				perishableEvent.m_perceptionEvent = rememberedEvent;

				// set the expiration time on our perishable event
				perishableEvent.m_expirationTime = expiryTime;

				if (eventList == null)
				{
					// create an new list, add the event and add the list to our dictionary
					eventList = new List<PerishablePerceptionEvent> { perishableEvent };
					m_rememberedEvents.Add(perishableEvent.m_perceptionEvent.Action, eventList);
				}
				else
				{
					// add to the existing list
					eventList.Add(perishableEvent);
				}
			}
		}
	}

	// --------------------------------------------------------------------------------

	public void RemoveExpiredMemories()
	{
		float currentTime = Time.time;

		// iterate dictionary
		foreach (var kvp in m_rememberedEvents)
		{
			// iterate perishable events
			for (int i = 0; i < kvp.Value.Count; ++i)
			{
				// check for expiry
				if (kvp.Value[i].m_expirationTime <= currentTime)
				{
					// return perception events to pool
					PerceptionEvent.Pool.Return(kvp.Value[i].m_perceptionEvent);
					PerishablePerceptionEvent.Pool.Return(kvp.Value[i]);
					// null entry in list
					kvp.Value[i] = null;

					Debug.LogFormat("[HistoricMemory] {0} memory forgotten ({1})\n", kvp.Key, currentTime);
				}
			}
			// remove all nulled entries in list
			kvp.Value.RemoveAll(e => e == null);
		}
	}

	// --------------------------------------------------------------------------------

	private void LogState()
	{
		Debug.Log("[HistoricMemory] Current state:\n");
		foreach (var kvp in m_rememberedEvents)
		{
			Debug.LogFormat(" ---- [{0}] ({1})\n", kvp.Key, kvp.Value.Count);
			for (int i = 0; i < kvp.Value.Count; ++i)
			{
				Debug.LogFormat("    - Expiry: {0}, \n", kvp.Value[i].m_expirationTime);
				Agent actor = kvp.Value[i].m_perceptionEvent.Actor;
				Debug.LogFormat("    - Actor: {0}, \n", (actor != null ? actor.name : "None"));
				Debug.LogFormat("    - Location: {0}, \n", kvp.Value[i].m_perceptionEvent.Location);
				Agent target = kvp.Value[i].m_perceptionEvent.Target;
				Debug.LogFormat("    - Target: {0}, \n", (target != null ? target.name : "None"));
			}
		}
	}

}