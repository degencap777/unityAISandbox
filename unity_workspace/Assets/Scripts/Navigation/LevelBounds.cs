using UnityEngine;

[CreateAssetMenu(fileName = "LevelBounds", menuName = "LevelBounds", order = 1)]
public class LevelBounds : ScriptableObject
{

	[SerializeField]
	private Vector3 m_minBounds = Vector3.zero;
	public Vector3 MinBounds { get { return m_minBounds; } }

	[SerializeField]
	private Vector3 m_maxBounds = Vector3.zero;
	public Vector3 MaxBounds { get { return m_maxBounds; } }

	// --------------------------------------------------------------------------------

	public Vector3 Dimension { get { return m_maxBounds - m_minBounds; } }

}