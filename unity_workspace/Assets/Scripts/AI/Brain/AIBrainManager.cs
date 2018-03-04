using UnityEngine;

public class AIBrainManager : SingletonMonoBehaviour<AIBrainManager>
{
	
	private DistributedAIBrainUpdater m_brainUpdater = null;

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		m_brainUpdater = GetComponent<DistributedAIBrainUpdater>();
		Debug.Assert(m_brainUpdater != null, "[AIBrainManager::OnAwake] GetComponent<DistributedAIBrainUpdater> failed\n");
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		if (m_brainUpdater != null)
		{
			m_brainUpdater.OnUpdate();
		}
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