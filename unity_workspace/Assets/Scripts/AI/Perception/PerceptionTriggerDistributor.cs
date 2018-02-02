using System.Collections.Generic;

public class PerceptionTriggerDistributor : SingletonMonoBehaviour<PerceptionTriggerDistributor>
{

	private List<Perception> m_perceptions = new List<Perception>();
	private List<PerceptionTrigger> m_triggers = new List<PerceptionTrigger>();

	// --------------------------------------------------------------------------------

	public void RegisterPerception(Perception perception)
	{
		if (m_perceptions.Contains(perception))
		{
			return;
		}
		m_perceptions.Add(perception);
	}

	// --------------------------------------------------------------------------------

	public void UnregisterPerception(Perception perception)
	{
		m_perceptions.Remove(perception);
	}

	// --------------------------------------------------------------------------------

	public void DistributeTrigger(PerceptionTrigger trigger)
	{
		m_triggers.Add(trigger);
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		for (int t = 0; t < m_triggers.Count; ++t)
		{
			for (int p = 0; p < m_perceptions.Count; ++p)
			{
				m_perceptions[p].Percieve(m_triggers[t]);
			}

			// return to pool
			PerceptionTrigger.Pool.Return(m_triggers[t]);
		}

		// clear list of triggers
		m_triggers.Clear();
	}
	
}
