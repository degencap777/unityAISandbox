using UnityEngine;

public class AgentRotateAction : AgentAction
{

	[SerializeField, Range(-1, 1)]
	private int m_modifier = 1;

	// --------------------------------------------------------------------------------

	public override void Execute(float value)
	{
		if (m_agentController == null)
		{
			return;
		}

		m_agentController.Rotate(value * m_modifier);
	}

}