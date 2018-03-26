using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AllegianceManager : SingletonMonoBehaviour<AllegianceManager>
{

	[SerializeField]
	private List<Allegiance> m_allegiances = new List<Allegiance>();
	
	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		;
	}
	
}
