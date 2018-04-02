using System;
using UnityEngine;

namespace AISandbox.Utility
{
	[Serializable]
	public class AreaBounds : System.Object
	{

		[SerializeField]
		private Vector3 m_minBounds = Vector3.zero;
		public Vector3 MinBounds { get { return m_minBounds; } }

		[SerializeField]
		private Vector3 m_maxBounds = Vector3.zero;
		public Vector3 MaxBounds { get { return m_maxBounds; } }

		// --------------------------------------------------------------------------------

		public Vector3 Size { get { return m_maxBounds - m_minBounds; } }

		// --------------------------------------------------------------------------------
		
		public AreaBounds(Vector3 min, Vector3 max)
		{
			m_minBounds = min;
			m_maxBounds = max;
		}

		// --------------------------------------------------------------------------------

		public bool Contains(Vector3 position)
		{
			return position.x >= m_minBounds.x && position.y >= m_minBounds.y && position.z >= m_minBounds.z &&
				position.x <= m_maxBounds.x && position.y <= m_maxBounds.y && position.z <= m_maxBounds.z;
		}

	}
}