using System;
using UnityEngine;

[Serializable]
public abstract class InputAction
{

	[SerializeField]
	private AgentController m_agentController = null;

	// --------------------------------------------------------------------------------

	abstract public void Execute();

}
