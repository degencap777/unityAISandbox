using UnityEngine;

public class Behaviour_Pursue : Behaviour
{

	[SerializeField]
	private float m_pursueSuccessDistance = 5.0f;

	[SerializeField]
	private float m_pursueTriggerDistance = 10.0f;

	// --------------------------------------------------------------------------------

	private Agent m_target = null;
	public Agent Target
	{
		get { return m_target; }
		set { m_target = value; }
	}

	// --------------------------------------------------------------------------------

	public Behaviour_Pursue(Agent owner)
		: base(owner)
	{
		m_goal = new Goal_Pursue();
	}

	// --------------------------------------------------------------------------------

	public override void SetGoal()
	{
		Goal_Pursue pursueGoal = m_goal as Goal_Pursue;
		if (pursueGoal != null)
		{
			pursueGoal.Target = m_target;
			pursueGoal.SuccessDistance = m_pursueSuccessDistance;
			pursueGoal.ActivateDistance = m_pursueTriggerDistance;
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
