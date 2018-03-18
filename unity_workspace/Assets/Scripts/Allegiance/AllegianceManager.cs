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
		;
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

	// --------------------------------------------------------------------------------

	public void Editor_RemoveAllegiance(string name)
	{
		m_allegiances.RemoveAll(allegiance => string.Compare(allegiance.Name, name) == 0);
	}

	// --------------------------------------------------------------------------------

	public void Editor_CreateAllegiance()
	{
		m_allegiances.Add(new Allegiance());
	}

#endif // UNITY_EDITOR

}
