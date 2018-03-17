using UnityEngine;

public class Scalar : ScriptableObject
{

	[SerializeField]
	private float m_multiplier = 1.0f;
	public float Multiplier { get { return m_multiplier; } }

	// --------------------------------------------------------------------------------

	public float ScaleValue(float value)
	{
		return value * m_multiplier;
	}

}
