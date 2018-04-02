using UnityEngine;

namespace AISandbox.Allegiance
{
	[CreateAssetMenu(fileName = "AllegianceConfig", menuName = "Component Config/Allegiance", order = 1)]
	public class AllegianceConfig : ScriptableObject
	{

		[SerializeField]
		private string m_name = string.Empty;
		public string Name { get { return m_name; } }

		[SerializeField]
		private Color m_colour = Color.white;
		public Color Colour { get { return m_colour; } }

	}
}