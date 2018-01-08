using UnityEngine;

public class Behaviour_Evade : Behaviour
{

	[SerializeField]
	private float m_successDistance = 5.0f;
	private float m_successDistanceSquared = 25.0f;
	
	// --------------------------------------------------------------------------------

	public Behaviour_Evade(WorkingMemory workingMemory)
		: base(workingMemory)
	{
		m_goal = new Goal_Evade();
		SetGoal();

		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	protected override void SetGoal()
	{
		Goal_Evade evadeGoal = m_goal as Goal_Evade;
		if (evadeGoal != null)
		{
			evadeGoal.WorkingMemory = m_workingMemory;
			evadeGoal.SuccessDistanceSquared = m_successDistanceSquared;
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
			// #SteveD >>> todo
		}
	}
	
}
