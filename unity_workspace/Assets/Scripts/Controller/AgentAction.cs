using UnityEngine;

public abstract class AgentAction : MonoBehaviour
{

	[SerializeField]
	protected MovementComponent m_agentController = null;

	// --------------------------------------------------------------------------------

	abstract public void Execute(float value);

}
