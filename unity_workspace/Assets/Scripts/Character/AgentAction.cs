using AISandbox.Movement;
using UnityEngine;

namespace AISandbox.Character
{
	public abstract class AgentAction : MonoBehaviour
	{

		[SerializeField]
		protected MovementComponent m_agentController = null;

		// --------------------------------------------------------------------------------

		abstract public void Execute(float value);

	}
}