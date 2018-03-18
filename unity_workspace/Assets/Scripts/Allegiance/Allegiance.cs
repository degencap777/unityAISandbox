using System;
using UnityEngine;

[Serializable]
public class Allegiance : IComparable<Allegiance>
{

	public static readonly int k_noAllegianceId = -1;
	public static readonly string k_noAllegianceName = "NOT SET";

	// --------------------------------------------------------------------------------

	[SerializeField]
	private string m_name = k_noAllegianceName;
	public string Name { get { return m_name; } }

	[SerializeField]
	private Color m_colour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public Color Colour { get { return m_colour; } }

	// --------------------------------------------------------------------------------

	[SerializeField, HideInInspector]
	private int m_id = k_noAllegianceId;
	public int Id { get { return m_id; } }

	// --------------------------------------------------------------------------------

	public Allegiance(int id)
	{
		m_id = id;
	}

	// --------------------------------------------------------------------------------

	public int CompareTo(Allegiance other)
	{
		return Mathf.Clamp(other.m_id - m_id, -1, 1);
	}

}