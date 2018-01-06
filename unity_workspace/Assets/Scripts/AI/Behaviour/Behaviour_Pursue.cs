using UnityEngine;

public class Behaviour_Pursue : Behaviour
{

	[SerializeField]
	private float m_pursueSuccessDistance = 5.0f;

	[SerializeField]
	private float m_pursueTriggerDistance = 10.0f;

	// --------------------------------------------------------------------------------

	public Behaviour_Pursue(WorkingMemory workingMemory)
		: base(workingMemory)
	{
		m_goal = new Goal_Pursue();
		SetGoal();
	}

	// --------------------------------------------------------------------------------

	protected override void SetGoal()
	{
		Goal_Pursue pursueGoal = m_goal as Goal_Pursue;
		if (pursueGoal != null)
		{
			pursueGoal.WorkingMemory = m_workingMemory;
			pursueGoal.SuccessDistance = m_pursueSuccessDistance;
			pursueGoal.ActivateDistance = m_pursueTriggerDistance;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.SortTargets();
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnExit()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		Agent target = GetHighestPriorityTarget();
		if (target != null)
		{
			// #SteveD >>>> todo
		}
	}
	
}
