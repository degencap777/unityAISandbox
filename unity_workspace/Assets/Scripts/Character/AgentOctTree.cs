using AISandbox.Character;
using AISandbox.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AISandbox.Container
{
	public class AgentOctTree : MonoBehaviour
	{

		[SerializeField]
		private AreaBounds m_bounds = new AreaBounds(Vector3.zero, Vector3.zero);

		[SerializeField]
		private int m_splitTrigger = 10;

		[SerializeField]
		private int m_combineTrigger = 8;

		[SerializeField]
		private int m_maxDepth = 4;

		[Header("Statistics")]
		[SerializeField]
		private OctTreeStatistics m_statistics = null;

		// ----------------------------------------------------------------------------

		private OctTree m_octTree = null;

		// ----------------------------------------------------------------------------

		// worker lists
		private List<OctTreeOccupant> m_migrants = new List<OctTreeOccupant>(32);
		private List<OctTree> m_leafNodes = new List<OctTree>(128);
		private List<OctTree> m_parentNodes = new List<OctTree>(128);
		private bool m_nodeListsDirty = true;

		// ----------------------------------------------------------------------------

		protected virtual void Awake()
		{
			m_octTree = new OctTree(null, m_bounds);

			if (m_statistics != null)
			{
				m_statistics.Reset();
			}
		}

		// ----------------------------------------------------------------------------

		protected virtual void Start()
		{
			if (m_octTree == null)
			{
				Debug.LogError("[AgentOctTree::Start] m_octTree is null");
				return;
			}
			
			var occupants = FindObjectsOfType<OctTreeOccupant>();
			for (int i = 0; i < occupants.Length; ++i)
			{
				m_octTree.Insert(occupants[i]);
			}
		}

		// ----------------------------------------------------------------------------

		protected virtual void Update()
		{
			if (m_nodeListsDirty)
			{
				m_leafNodes.Clear();
				m_octTree.GetAllLeafNodes(m_leafNodes);

				m_parentNodes.Clear();
				m_octTree.GetAllParentNodes(m_parentNodes);

				m_nodeListsDirty = false;
			}

			UpdateMigration();
			UpdateDivision();

			if (m_statistics != null)
			{
				// #SteveD	>>> implement node counting functions
				int nodes = 0;//m_octTree.CountNodes();
				int leafNodes = 0;// m_octTree.CountLeafNodes();
				// <<<<<<<<<<<<
				
				m_statistics.m_currentNodes = nodes;
				m_statistics.m_peakNodes = Mathf.Max(nodes, m_statistics.m_peakNodes);
				m_statistics.m_currentLeafNodes = leafNodes;
				m_statistics.m_peakLeafNodes = Mathf.Max(leafNodes, m_statistics.m_peakLeafNodes);
			}
		}

		// ----------------------------------------------------------------------------

		private void UpdateMigration()
		{
			if (m_octTree == null)
			{
				return;
			}
			
			// capture migrants
			m_octTree.CaptureMigrants(m_migrants);

			if (m_statistics != null)
			{
				// report migrants
				m_statistics.m_currentMigrants = m_migrants.Count;
				if (m_statistics.m_currentMigrants > m_statistics.m_peakMigrants)
				{
					m_statistics.m_peakMigrants = m_statistics.m_currentMigrants;
				}
			}

			// redistribute migrants
			for (int i = 0; i < m_migrants.Count; ++i)
			{
				m_octTree.Insert(m_migrants[i]);
			}

			// clear migrants worker list
			m_migrants.Clear();
		}

		// ----------------------------------------------------------------------------

		private void UpdateDivision()
		{
			if (m_octTree == null)
			{
				return;
			}

			// split
			for (int i = 0; i < m_leafNodes.Count; ++i)
			{
				if (m_leafNodes[i].GetOccupantCount() >= m_splitTrigger && m_leafNodes[i].GetDepth() < m_maxDepth)
				{
					m_leafNodes[i].Split();
					m_nodeListsDirty = true;
				}
			}

			// combine
			for (int i = 0; i < m_parentNodes.Count; ++i)
			{
				if (m_parentNodes[i].GetOccupantCount() <= m_combineTrigger)
				{
					m_parentNodes[i].Combine();
					m_nodeListsDirty = true;
				}
			}
		}

		// ----------------------------------------------------------------------------
		// Editor specific ------------------------------------------------------------

#if UNITY_EDITOR

		[Header("Debug")]
		[SerializeField]
		private Color m_gizmoColour = Color.green;

		// ----------------------------------------------------------------------------

		protected virtual void OnDrawGizmosSelected()
		{
			if (m_octTree != null)
			{
				Color cachedColour = Gizmos.color;
				Gizmos.color = m_gizmoColour;

				m_octTree.GizmoDrawBounds();

				Gizmos.color = cachedColour;
			}
		}

#endif // UNITY_EDITOR

	}
}
