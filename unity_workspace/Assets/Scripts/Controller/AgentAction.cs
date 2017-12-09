using UnityEngine;

public abstract class AgentAction : MonoBehaviour
{

	[SerializeField]
	protected AgentController m_agentController = null;

	// --------------------------------------------------------------------------------

	abstract public void Execute();

}
