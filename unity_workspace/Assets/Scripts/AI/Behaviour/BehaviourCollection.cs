using System.Collections.Generic;
using UnityEngine;

public class BehaviourCollection : MonoBehaviour
{

	[SerializeField]
	private BehaviourId m_initialBehaviour = BehaviourId.None;

	private Dictionary<BehaviourId, Behaviour> m_behaviours = new Dictionary<BehaviourId, Behaviour>();
	private Behaviour m_currentBehaviour = null;

	// --------------------------------------------------------------------------------
	
	protected void Awake()
	{
		var allBehaviours = gameObject.GetComponentsInChildren<Behaviour>();
		Behaviour currentBehaviour = null;

		for (int i = 0; i < allBehaviours.Length; ++i)
		{
			currentBehaviour = allBehaviours[i];
			if (m_behaviours.ContainsKey(currentBehaviour.BehaviourId))
			{
				Debug.LogErrorFormat("[BehaviourCollecion] Attempting to add duplicate behaviour to BehaviourCollection {0}\n",
					currentBehaviour.BehaviourId);
			}
			else
			{
				m_behaviours.Add(currentBehaviour.BehaviourId, currentBehaviour);
			}
		}
	}

	// --------------------------------------------------------------------------------

	public void OnStart(Agent owner, WorkingMemory memory)
	{
		foreach (Behaviour behaviour in m_behaviours.Values)
		{
			behaviour.OnStart(owner, memory);
		}

		m_behaviours.TryGetValue(m_initialBehaviour, out m_currentBehaviour);
		if (m_currentBehaviour != null)
		{
			m_currentBehaviour.OnEnter();
		}
	}

	// --------------------------------------------------------------------------------
	
	public void OnUpdate()
	{
		if (m_currentBehaviour != null)
		{
			m_currentBehaviour.OnUpdate();
		}
	}

}
