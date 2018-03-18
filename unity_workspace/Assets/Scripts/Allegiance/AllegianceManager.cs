using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AllegianceManager : SingletonMonoBehaviour<AllegianceManager>
{

	[SerializeField, HideInInspector]
	private List<Allegiance> m_allegiances = new List<Allegiance>();
	
	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
#if UNITY_EDITOR
		Editor_Awake();
#endif // UNITY_EDITOR
	}

	// --------------------------------------------------------------------------------

	public Allegiance GetAllegiance(string allegianceName)
	{
		for (int i = 0; i < m_allegiances.Count; ++i)
		{
			if (string.Compare(m_allegiances[i].Name, allegianceName) == 0)
			{
				return m_allegiances[i];
			}
		}

		return null;
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private int m_nextUid = 0;
	
	// --------------------------------------------------------------------------------

	protected virtual void Editor_Awake()
	{
		m_nextUid = 0;
		for (int i = 0; i < m_allegiances.Count; ++i)
		{
			m_nextUid = m_nextUid <= m_allegiances[i].Id ? m_allegiances[i].Id + 1 : m_nextUid;
		}
	}

	// --------------------------------------------------------------------------------

	public void Editor_RemoveAllegiance(int id)
	{
		m_allegiances.RemoveAll(allegiance => allegiance.Id == id);
	}

	// --------------------------------------------------------------------------------

	public void Editor_CreateAllegiance()
	{
		m_allegiances.Add(new Allegiance(m_nextUid++));
	}

#endif // UNITY_EDITOR

}
