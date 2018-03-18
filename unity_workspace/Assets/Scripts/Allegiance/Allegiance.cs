using System;
using UnityEngine;

[Serializable]
public class Allegiance : IComparable<Allegiance>
{

	public static readonly string k_noAllegianceName = "NOT SET";

	// --------------------------------------------------------------------------------

	[SerializeField]
	private string m_name = k_noAllegianceName;
	public string Name { get { return m_name; } }

	[SerializeField]
	private Color m_colour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public Color Colour { get { return m_colour; } }
	
	// --------------------------------------------------------------------------------

	public int CompareTo(Allegiance other)
	{
		return Mathf.Clamp(string.Compare(m_name, other.Name), -1, 1);
	}

}