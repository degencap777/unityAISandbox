using UnityEngine;

[CreateAssetMenu(fileName = "AllegianceSettings", menuName = "Component Settings/Allegiance", order = 1)]
public class AllegianceSettings : ScriptableObject
{

	[SerializeField]
	private Allegiance m_allegiance = null;
	public Allegiance Allegiance { get { return m_allegiance; } }

}