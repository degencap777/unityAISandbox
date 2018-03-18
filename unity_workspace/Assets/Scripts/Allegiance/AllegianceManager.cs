using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AllegianceManager : MonoBehaviour
{

	[SerializeField, HideInInspector]
	private List<Allegiance> m_allegiances = new List<Allegiance>();
	public List<Allegiance>.Enumerator AllegianceEnumerator { get { return m_allegiances.GetEnumerator(); } }

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
#if UNITY_EDITOR
		Editor_Awake();
#endif // UNITY_EDITOR
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
