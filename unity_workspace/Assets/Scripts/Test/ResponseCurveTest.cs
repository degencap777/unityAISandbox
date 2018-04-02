using AISandbox.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AISandbox.Test
{
	public class ResponseCurveTest : Test
	{

		[SerializeField]
		[Range(0.01f, 1.0f)]
		private float m_step = 0.1f;

		// --------------------------------------------------------------------------------

		private List<ResponseCurve> m_responseCurves = new List<ResponseCurve>();

		// --------------------------------------------------------------------------------

		protected override void Awake()
		{
			GetComponentsInChildren(false, m_responseCurves);
		}

		// ------------------------------------------------------------------------------------

		protected override void RunTests()
		{
			for (int i = 0; i < m_responseCurves.Count; ++i)
			{
				for (float f = 0.0f; f <= 1.0f + m_step; f += m_step)
				{
					Debug.LogFormat("[ResponseCurve] [{0}] value at {1} = {2}", i, f.ToString("N2"), m_responseCurves[i].GetValue(f));
					Debug.LogFormat("[ResponseCurve] [{0}] normalised value at {1} = {2}", i, f.ToString("N2"), m_responseCurves[i].GetValue(f));
				}
			}
		}

		// ------------------------------------------------------------------------------------

		protected override void ResetTests()
		{
			;
		}

	}
}