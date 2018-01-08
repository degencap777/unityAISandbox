using UnityEngine;

public class Behaviour_Pursue : Behaviour
{

	[SerializeField]
	private float m_successDistance = 2.0f;
	private float m_successDistanceSquared = 4.0f;

	// --------------------------------------------------------------------------------

	public Behaviour_Pursue(WorkingMemory workingMemory)
		: base(workingMemory)
	{
		m_goal = new Goal_Pursue();
		SetGoal();

		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	protected override void SetGoal()
	{
		Goal_Pursue pursueGoal = m_goal as Goal_Pursue;
		if (pursueGoal != null)
		{
			pursueGoal.WorkingMemory = m_workingMemory;
			pursueGoal.SuccessDistanceSquared = m_successDistance;
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
