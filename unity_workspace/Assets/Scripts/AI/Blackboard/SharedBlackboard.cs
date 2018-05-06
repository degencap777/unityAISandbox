using AISandbox.Container;
using AISandbox.Navigation;
using UnityEngine;

namespace AISandbox.AI
{
	public class SharedBlackboard : MonoBehaviour
	{

		[SerializeField]
		private NavMesh m_navMesh = null;
		public NavMesh NavMesh { get { return m_navMesh; } }

		[SerializeField]
		private OctTree m_octTree = null;
		public OctTree OctTree { get { return m_octTree; } }

	}
}