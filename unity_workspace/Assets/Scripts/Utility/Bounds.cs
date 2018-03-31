using UnityEngine;

[CreateAssetMenu(fileName = "Bounds", menuName = "Bounds", order = 1)]
public class Bounds : ScriptableObject
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