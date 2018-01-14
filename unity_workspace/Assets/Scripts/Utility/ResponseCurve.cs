using System.Collections.Generic;
using UnityEngine;

public class ResponseCurve : MonoBehaviour
{

	public List<float> m_edges = new List<float>();

	// --------------------------------------------------------------------------------

	public float GetValue(float position)
	{
		float result = 0.0f;

		if (m_edges.Count == 0)
		{
			return 0.0f;
		}
		else if (m_edges.Count == 1)
		{
			return m_edges[0];
		}

		// #SteveD >>> find bucket edges

		// #SteveD >>> find value between bucket's edges

		return result;
	}
	
}
