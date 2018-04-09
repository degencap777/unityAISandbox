using System.Collections.Generic;
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

		private OctTree m_octTree = null;

		// --------------------------------------------------------------------------------

		// worker lists
		private List<OctTreeOccupant> m_migrants = new List<OctTreeOccupant>();

		// --------------------------------------------------------------------------------

		public void Initialise(OctTree octTree)
		{
			m_octTree = octTree;

			m_peakMigrants = 0;
			m_currentMigrants = 0;
			m_peakNodes = 0;
			m_currentNodes = 0;
			m_peakLeafNodes = 0;
			m_currentLeafNodes = 0;
		}

		// --------------------------------------------------------------------------------

		public void UpdateNodeCounts()
		{
			if (m_octTree == null)
			{
				return;
			}

			int nodes = m_octTree.CountNodes(false);
			int leafNodes = m_octTree.CountNodes(true);

			m_currentNodes = nodes;
			m_peakNodes = Mathf.Max(nodes, m_peakNodes);
			m_currentLeafNodes = leafNodes;
			m_peakLeafNodes = Mathf.Max(leafNodes, m_peakLeafNodes);
		}

		// --------------------------------------------------------------------------------

		public void UpdateMigrants()
		{
			if (m_octTree == null)
			{
				return;
			}

			m_octTree.CaptureMigrants(m_migrants);
			m_currentMigrants = m_migrants.Count;
			m_migrants.Clear();
		}

		// --------------------------------------------------------------------------------
		// Editor specific ----------------------------------------------------------------

#if UNITY_EDITOR

		public void Editor_Reset()
		{
			Initialise(m_octTree);
			UpdateNodeCounts();
			UpdateMigrants();
		}

#endif // UNITY_EDITOR

	}
}