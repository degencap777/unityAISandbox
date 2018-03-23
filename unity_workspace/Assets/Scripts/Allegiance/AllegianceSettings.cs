using UnityEngine;

[CreateAssetMenu(fileName = "AllegianceSettings", menuName = "Component Settings/Allegiance", order = 1)]
public class AllegianceSettings : ScriptableObject
{

	[SerializeField]
	private string m_allegianceName = Allegiance.k_noAllegianceName;
	public string AllegianceName { get { return m_allegianceName; } }

}