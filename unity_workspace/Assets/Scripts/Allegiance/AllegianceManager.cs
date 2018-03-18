using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AllegianceManager : MonoBehaviour
{

	private static int m_nextUid = 0;

	public static int GetNextUid()
	{
		return m_nextUid++;
	}

	// --------------------------------------------------------------------------------

	[SerializeField]
	private List<Allegiance> m_allegiances = new List<Allegiance>();

	// --------------------------------------------------------------------------------

	// #SteveD	>>> editor functionality - add allegiance (call Allegiance constructor using GetNextUid())
	//			>>> custom editor with Add allegiance button at bottom of list, remove allegiance button next to each allegiance
	//			>>> property drawer for allegiance (one row with background in allegiance colour, name in textbox, change colour button, remove button)

}
