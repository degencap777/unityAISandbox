using System.Collections.Generic;
using UnityEngine;

namespace AISandbox.Utility
{
	public class ResponseCurve : MonoBehaviour
	{

		public List<float> m_edges = new List<float>();

		// --------------------------------------------------------------------------------

		public float GetValue(float normalisedPosition)
		{
			// handle not enough edges
			if (m_edges.Count == 0)
			{
				return 0.0f;
			}
			else if (m_edges.Count == 1)
			{
				return m_edges[0];
			}

			// handle invalid normalised position
			if (normalisedPosition <= 0.0f)
			{
				return m_edges[0];
			}
			else if (normalisedPosition >= 1.0f)
			{
				return m_edges[m_edges.Count - 1];
			}

			// calculate bucket
			float bucketWidth = 1.0f / (m_edges.Count - 1);
			// calculate bucket min edge
			int minEdge = Mathf.FloorToInt(normalisedPosition / bucketWidth);

			// calculate min, max and diff values
			float min = m_edges[minEdge];
			float max = m_edges[minEdge + 1];
			float diff = max - min;

			// calculate value within bucket
			float result = min + (diff * ((normalisedPosition - (minEdge * bucketWidth)) / bucketWidth));
			return result;
		}

		// --------------------------------------------------------------------------------

		public float GetValueNormalised(float normalisedPosition)
		{
			float maxValue = GetMaxValue();

			return maxValue == 0.0f ?
				0.0f :
				GetValue(normalisedPosition) / maxValue;
		}

		// --------------------------------------------------------------------------------

		private float GetMaxValue()
		{
			float maxValue = m_edges.Count > 0 ? m_edges[0] : 0.0f;
			for (int i = 1; i < m_edges.Count; ++i)
			{
				if (m_edges[i] > maxValue)
				{
					maxValue = m_edges[i];
				}
			}
			return maxValue;
		}

	}
}