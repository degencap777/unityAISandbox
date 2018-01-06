using UnityEngine;

public class Behaviour_Evade : Behaviour
{

	[SerializeField]
	private float m_evadeSuccessDistance = 25.0f;

	[SerializeField]
	private float m_evadeTriggerDistance = 23.0f;
	
	// --------------------------------------------------------------------------------

	public Behaviour_Evade(WorkingMemory workingMemory)
		: base(workingMemory)
	{
		m_goal = new Goal_Evade();
		SetGoal();
	}

	// --------------------------------------------------------------------------------

	protected override void SetGoal()
	{
		Goal_Evade evadeGoal = m_goal as Goal_Evade;
		if (evadeGoal != null)
		{
			evadeGoal.WorkingMemory = m_workingMemory;
			evadeGoal.SuccessDistance = m_evadeSuccessDistance;
			evadeGoal.ActivateDistance = m_evadeTriggerDistance;
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
