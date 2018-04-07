using AISandbox.Container;
using UnityEngine;
using UnityEngine.UI;

namespace AISandbox.UI
{
	public class OctTreeStatsDialog : MonoBehaviour
	{

		private static readonly string k_currentTemplate = "Current [{0}]";
		private static readonly string k_peakTemplate = "Peak [{0}]";

		// ----------------------------------------------------------------------------

		[SerializeField]
		private OctTreeStatistics m_stats = null;

		[SerializeField]
		private Text m_nodesCurrentText = null;
		[SerializeField]
		private Text m_nodesPeakText = null;

		[SerializeField]
		private Text m_leafNodesCurrentText = null;
		[SerializeField]
		private Text m_leafNodesPeakText = null;

		[SerializeField]
		private Text m_migrantsCurrentText = null;
		[SerializeField]
		private Text m_migrantsPeakText = null;

		// ----------------------------------------------------------------------------

		private int m_nodesCurrent = 0;
		private int m_nodesPeak = 0;

		private int m_leafNodesCurrent = 0;
		private int m_leafNodesPeak = 0;

		private int m_migrantsCurrent = 0;
		private int m_migrantsPeak = 0;

		// ----------------------------------------------------------------------------

		protected virtual void Start()
		{
			if (m_stats != null)
			{
				m_nodesCurrent = m_stats.m_currentNodes;
				SetText(m_nodesCurrentText, k_currentTemplate, m_nodesCurrent);
				m_nodesPeak = m_stats.m_peakNodes;
				SetText(m_nodesPeakText, k_peakTemplate, m_nodesPeak);

				m_leafNodesCurrent = m_stats.m_currentLeafNodes;
				SetText(m_leafNodesCurrentText, k_currentTemplate, m_leafNodesCurrent);
				m_leafNodesPeak = m_stats.m_peakLeafNodes;
				SetText(m_leafNodesPeakText, k_peakTemplate, m_leafNodesPeak);

				m_migrantsCurrent = m_stats.m_currentMigrants;
				SetText(m_migrantsCurrentText, k_currentTemplate, m_migrantsCurrent);
				m_migrantsPeak = m_stats.m_peakMigrants;
				SetText(m_migrantsPeakText, k_peakTemplate, m_migrantsPeak);
			}
		}

		// ----------------------------------------------------------------------------

		protected virtual void Update()
		{
			if (m_stats != null)
			{
				if (m_nodesCurrent != m_stats.m_currentNodes)
				{
					m_nodesCurrent = m_stats.m_currentNodes;
					SetText(m_nodesCurrentText, k_currentTemplate, m_nodesCurrent);
				}

				if (m_nodesPeak != m_stats.m_peakNodes)
				{
					m_nodesPeak = m_stats.m_peakNodes;
					SetText(m_nodesPeakText, k_peakTemplate, m_nodesPeak);
				}

				if (m_leafNodesCurrent != m_stats.m_currentLeafNodes)
				{
					m_leafNodesCurrent = m_stats.m_currentLeafNodes;
					SetText(m_leafNodesCurrentText, k_currentTemplate, m_leafNodesCurrent);
				}

				if (m_leafNodesPeak != m_stats.m_peakLeafNodes)
				{
					m_leafNodesPeak = m_stats.m_peakLeafNodes;
					SetText(m_leafNodesPeakText, k_peakTemplate, m_leafNodesPeak);
				}

				if (m_migrantsCurrent != m_stats.m_currentMigrants)
				{
					m_migrantsCurrent = m_stats.m_currentMigrants;
					SetText(m_migrantsCurrentText, k_currentTemplate, m_migrantsCurrent);
				}

				if (m_migrantsPeak != m_stats.m_peakMigrants)
				{
					m_migrantsPeak = m_stats.m_peakMigrants;
					SetText(m_migrantsPeakText, k_peakTemplate, m_migrantsPeak);
				}
			}
		}

		// ----------------------------------------------------------------------------

		private void SetText(Text text, string template, int count)
		{
			if (text != null)
			{
				text.text = string.Format(template, count);
			}
		}

	}
}
