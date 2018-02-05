using UnityEngine;

public class AIBrainManager : SingletonMonoBehaviour<AIBrainManager>
{
	
	// --------------------------------------------------------------------------------

	private DistributedAIBrainUpdater m_brainUpdater = null;

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		m_brainUpdater = GetComponent<DistributedAIBrainUpdater>();
		Debug.Assert(m_brainUpdater != null, "[AIBrainManager::OnAwake] GetComponent<DistributedUpdater<AIBrain>> failed\n");
	}

	// --------------------------------------------------------------------------------

	public void RegisterAIBrain(AIBrain brain)
	{
		if (m_brainUpdater != null)
		{
			m_brainUpdater.RegisterUpdatable(brain);
		}
	}

	// --------------------------------------------------------------------------------

	public void UnregisterAIBrain(AIBrain brain)
	{
		if (m_brainUpdater != null)
		{
			m_brainUpdater.UnregisterUpdatable(brain);
		}
	}

}