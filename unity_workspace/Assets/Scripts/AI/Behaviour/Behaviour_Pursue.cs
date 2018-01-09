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
		m_successDistanceSquared = m_successDistance * m_successDistance;

		m_goal = new Goal_Pursue();
		SetUpGoal();
	}

	// --------------------------------------------------------------------------------

	protected override void SetUpGoal()
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
		if (m_workingMemory == null)
		{
			return;
		}

		Agent owner = m_workingMemory.Owner;
		Agent target = m_workingMemory.GetHighestPriorityTarget();

		if (owner != null && target != null)
		{
			Vector3 toTarget = target.transform.position - owner.transform.position;

			// #SteveD >>> todo

		}
	}
	
}
