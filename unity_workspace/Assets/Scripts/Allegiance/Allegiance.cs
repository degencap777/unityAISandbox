using UnityEngine;

[CreateAssetMenu(fileName = "Allegiance", menuName = "Allegiance", order = 1)]
public class Allegiance : ScriptableObject
{

	[SerializeField]
	private string m_name = string.Empty;
	public string Name { get { return m_name; } }

	[SerializeField]
	private Color m_colour = Color.white;
	public Color Colour { get { return m_colour; } }

	// --------------------------------------------------------------------------------

	public Allegiance()
	{
		;
	}

}