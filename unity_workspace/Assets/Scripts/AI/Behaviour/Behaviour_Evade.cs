using UnityEngine;

public class Behaviour_Evade : Behaviour
{

	[SerializeField]
	private float m_evadeSuccessDistance = 25.0f;

	[SerializeField]
	private float m_evadeTriggerDistance = 23.0f;

	// --------------------------------------------------------------------------------

	private Agent m_pursuant = null;
	public Agent Pursuant
	{
		get { return m_pursuant; }
		set { m_pursuant = value; }
	}

	// --------------------------------------------------------------------------------

	public Behaviour_Evade(Agent owner)
		: base(owner)
	{
		m_goal = new Goal_Evade();
	}

	// --------------------------------------------------------------------------------

	public override void SetGoal()
	{
		Goal_Evade evadeGoal = m_goal as Goal_Evade;
		if (evadeGoal != null)
		{
			evadeGoal.Pursuant = m_pursuant;
			evadeGoal.SuccessDistance = m_evadeSuccessDistance;
			evadeGoal.ActivateDistance = m_evadeTriggerDistance;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnExit()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		// #SteveD >>> todo
	}
	
}
