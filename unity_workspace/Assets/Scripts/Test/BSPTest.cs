using System.Collections.Generic;
using UnityEngine;

public class BSPTest : Test
{

	[SerializeField]
	private BSP m_bsp = null;

	[SerializeField]
	private Agent m_agent = null;

	[SerializeField]
	private float m_radius = 1.0f;

	// --------------------------------------------------------------------------------

	private List<BSPPartition> m_partitions = new List<BSPPartition>();

	// --------------------------------------------------------------------------------

	protected override void ResetTests()
	{
		
	}

	// --------------------------------------------------------------------------------

	protected override void RunTests()
	{
		if (m_agent != null && m_bsp != null)
		{
			m_bsp.GetPartitionsForRadius(m_agent.Transform.position, m_radius, m_partitions);
		}
	}

	// --------------------------------------------------------------------------------
	// Editor specific ----------------------------------------------------------------

#if UNITY_EDITOR

	private static readonly Color k_gizmoColour = new Color(0.4f, 0.0f, 0.85f, 1.0f);

	// --------------------------------------------------------------------------------

	private void OnDrawGizmos()
	{
		Color cachedColour = Gizmos.color;
		Gizmos.color = k_gizmoColour;

		for (int i = 0; i < m_partitions.Count; ++i)
		{
			Vector3 min = m_partitions[i].MinBounds;
			Vector3 max = m_partitions[i].MaxBounds;
			Vector3 size = max - min;

			Gizmos.DrawLine(min, min + new Vector3(size.x, 0.0f, 0.0f));
			Gizmos.DrawLine(min, min + new Vector3(0.0f, size.y, 0.0f));
			Gizmos.DrawLine(min, min + new Vector3(0.0f, 0.0f, size.z));
			Gizmos.DrawLine(min + new Vector3(size.x, 0.0f, 0.0f), min + new Vector3(size.x, 0.0f, size.z));
			Gizmos.DrawLine(min + new Vector3(0.0f, size.y, 0.0f), min + new Vector3(0.0f, size.y, size.z));
			Gizmos.DrawLine(min + new Vector3(0.0f, 0.0f, size.z), min + new Vector3(size.x, 0.0f, size.z));
			Gizmos.DrawLine(min + new Vector3(0.0f, 0.0f, size.z), min + new Vector3(0.0f, size.y, size.z));
			Gizmos.DrawLine(max, max - new Vector3(size.x, 0.0f, 0.0f));
			Gizmos.DrawLine(max, max - new Vector3(0.0f, size.y, 0.0f));
			Gizmos.DrawLine(max, max - new Vector3(0.0f, 0.0f, size.z));
			Gizmos.DrawLine(max - new Vector3(0.0f, 0.0f, size.z), max - new Vector3(size.x, 0.0f, size.z));
			Gizmos.DrawLine(max - new Vector3(0.0f, 0.0f, size.z), max - new Vector3(0.0f, size.y, size.z));
		}

		Gizmos.color = cachedColour;
	}

#endif //UNITY_EDITOR

}
