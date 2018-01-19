using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourCollection : MonoBehaviour
{

	[SerializeField]
	private AIBehaviourId m_initialBehaviour = AIBehaviourId.None;

	[SerializeField, HideInInspector]
	private Dictionary<AIBehaviourId, AIBehaviour> m_behaviours = new Dictionary<AIBehaviourId, AIBehaviour>();

	[SerializeField, HideInInspector]
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

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public Dictionary<AIBehaviourId, AIBehaviour> Editor_Behaviours { get { return m_behaviours; } }
	public AIBehaviour Editor_CurrentBehaviour { get { return m_currentBehaviour; } }

	// --------------------------------------------------------------------------------

	public void Editor_ResetCurrentBehaviour()
	{
		if (m_currentBehaviour != null)
		{
			m_currentBehaviour.Reset();
		}
	}

	// --------------------------------------------------------------------------------

	public void Editor_ResetCollection()
	{
		// exit current behaviour
		if (m_currentBehaviour != null)
		{
			m_currentBehaviour.OnExit();
		}

		// reset all behaviours
		foreach (var behaviour in m_behaviours.Values)
		{
			behaviour.Reset();
		}

		// reset to starting behaviour
		m_behaviours.TryGetValue(m_initialBehaviour, out m_currentBehaviour);
		if (m_currentBehaviour != null)
		{
			// enter current behaviour
			m_currentBehaviour.OnEnter();
		}
	}

#endif // UNITY_EDITOR

}
