using UnityEngine;

namespace AISandbox.Container
{
	[CreateAssetMenu(fileName = "OctTreeStats", menuName = "Statistics/OctTree Statistics", order = 1)]
	public class OctTreeStatistics : ScriptableObject
	{

		public int m_peakMigrants = 0;
		public int m_currentMigrants = 0;

		public int m_peakNodes = 0;
		public int m_currentNodes = 0;

		public int m_peakLeafNodes = 0;
		public int m_currentLeafNodes = 0;

		// --------------------------------------------------------------------------------

		public void Reset()
		{
			m_peakMigrants = 0;
			m_currentMigrants = 0;
			m_peakNodes = 0;
			m_currentNodes = 0;
			m_peakLeafNodes = 0;
			m_currentLeafNodes = 0;
		}

		// --------------------------------------------------------------------------------
		// Editor specific ----------------------------------------------------------------

#if UNITY_EDITOR

		public void Editor_Reset()
		{
			Reset();
		}

#endif // UNITY_EDITOR

	}
}