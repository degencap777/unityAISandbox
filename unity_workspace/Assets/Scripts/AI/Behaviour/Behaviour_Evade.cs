using UnityEngine;

public class Behaviour_Evade : Behaviour
{

	public override BehaviourId BehaviourId { get { return BehaviourId.Evade; } }
	public override GoalFlags AchievedGoals { get { return GoalFlags.Escape; } }
	public override GoalFlags PrerequisiteGoals { get { return GoalFlags.None; } }

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_successDistance = 5.0f;
	private float m_successDistanceSquared = 25.0f;

	// --------------------------------------------------------------------------------

	// cache
	private Agent m_cachedTarget = null;
	private float m_toTargetSquared = 0.0f;

	// --------------------------------------------------------------------------------

	public override void Initialise(Agent owner, WorkingMemory workingMemory)
	{
		base.Initialise(owner, workingMemory);

		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------
	
	public override void OnEnter()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.SortTargets();
			m_cachedTarget = m_workingMemory.GetHighestPriorityTarget();
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

		// #SteveD >>> periodically poll for highest priority target in case it changes

		if (m_owner != null && m_owner.AgentController != null && m_cachedTarget != null)
		{
			Vector3 toTarget = m_cachedTarget.transform.position - m_owner.transform.position;
			m_toTargetSquared = toTarget.sqrMagnitude;

			// #SteveD >>> evade target

		}
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalAchieved()
	{
		return m_toTargetSquared >= m_successDistanceSquared;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalInvalidated()
	{
		return m_owner == null || m_cachedTarget == null;
	}

}
