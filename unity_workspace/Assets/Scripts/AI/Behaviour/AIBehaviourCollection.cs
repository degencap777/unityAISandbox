using System.Collections.Generic;
using UnityEngine;

// #SteveD >>> custom inspector:
//	- list of all available behaviours
//	- highlight current behaviour

public class AIBehaviourCollection : MonoBehaviour
{

	[SerializeField]
	private AIBehaviourId m_initialBehaviour = AIBehaviourId.None;

	private Dictionary<AIBehaviourId, AIBehaviour> m_behaviours = new Dictionary<AIBehaviourId, AIBehaviour>();
	private AIBehaviour m_currentBehaviour = null;

	// --------------------------------------------------------------------------------
	
	protected void Awake()
	{
		var allBehaviours = gameObject.GetComponentsInChildren<AIBehaviour>();
		AIBehaviour currentBehaviour = null;

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
		foreach (AIBehaviour behaviour in m_behaviours.Values)
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
