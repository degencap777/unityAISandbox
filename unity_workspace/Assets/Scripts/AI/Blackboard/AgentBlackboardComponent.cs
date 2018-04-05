using AISandbox.Component;
using UnityEngine;

namespace AISandbox.AI
{
	public class AgentBlackboardComponent : BaseComponent
	{

		[SerializeField]
		private AgentBlackboardConfig m_config = null;

		// --------------------------------------------------------------------------------

		private SharedBlackboard m_sharedBlackboard = null;
		public SharedBlackboard SharedBlackboard { get { return m_sharedBlackboard; } }

		private ComponentCollection m_componentCollection = null;
		public ComponentCollection ComponentCollection { get { return m_componentCollection; } }

		// --------------------------------------------------------------------------------

		public override void OnAwake()
		{
			if (m_config == null)
			{
				m_config = ScriptableObject.CreateInstance<AgentBlackboardConfig>();
			}
		}

		// --------------------------------------------------------------------------------

		public override void OnStart()
		{
			m_componentCollection = GetComponentInParent<ComponentCollection>();
			Debug.Assert(m_componentCollection != null, "[AgentBlackboardComponent::OnStart] GetComponentInParent<ComponentCollection>() failed\n");
		}

		// Editor specific ----------------------------------------------------------------
		// --------------------------------------------------------------------------------

#if UNITY_EDITOR

		public AgentBlackboardConfig Editor_Config { get { return m_config; } }

#endif // UNITY_EDITOR

	}
}