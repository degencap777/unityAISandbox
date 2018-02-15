using System;
using UnityEngine;

// #SteveD	>>> should probably make this a MonoBehaviour so that I can draw it in editor mode
//				and regenerate it in OnValidated

[Serializable]
public class OctSpacePartition<T>
{

	public struct Space
	{

		public Vector3 m_min;
		public Vector3 m_max;

		// ----------------------------------------------------------------------------

		public Space(Vector3 min, Vector3 max)
		{
			m_min = min;
			m_max = max;
		}

		// ----------------------------------------------------------------------------

		// #SteveD	>>> list of Ts in this partition

	}

	// --------------------------------------------------------------------------------

	[SerializeField]
	private Vector3 m_minExtents = Vector3.zero;
	[SerializeField]
	private Vector3 m_maxExtents = Vector3.zero;

	[SerializeField]
	private uint m_treeDepth = 3;

	// #SteveD	>>> tree for storing partitions

	// --------------------------------------------------------------------------------

	public bool Generate()
	{
		if (Validate())
		{
			SubdivideSpace();
			return true;
		}
		return false;
	}

	// --------------------------------------------------------------------------------

	private bool Validate()
	{
		if (m_maxExtents.x <= m_minExtents.x ||
			m_maxExtents.y <= m_minExtents.y ||
			m_maxExtents.z <= m_minExtents.z)
		{
			Debug.LogError("[OctTree] Invalid extents\n");
			return false;
		}

		if (m_treeDepth < 1)
		{
			Debug.LogErrorFormat("[OctTree] Invalid subdivision count: {0}\n", m_treeDepth);
			return false;
		}

		return true;
	}

	// --------------------------------------------------------------------------------

	private void SubdivideSpace()
	{
		// #SteveD	>>> grow our tree
	}

}
