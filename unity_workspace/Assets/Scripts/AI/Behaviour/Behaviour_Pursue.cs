using UnityEngine;

public class Behaviour_Pursue : Behaviour
{

	public override BehaviourId BehaviourId { get { return BehaviourId.Pursue; } }
	public override GoalFlags AchievedGoals { get { return GoalFlags.CloseDown; } }
	public override GoalFlags PrerequisiteGoals { get { return GoalFlags.None; } }

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_successDistance = 2.0f;
	private float m_successDistanceSquared = 4.0f;
	
	// --------------------------------------------------------------------------------

	// cache
	private Agent m_cachedTarget = null;
	private float m_toTargetSquared = float.MaxValue;

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

		if (m_owner != null && m_owner.AgentController != null)// && m_cachedTarget != null)
		{
			//Vector3 toTarget = m_cachedTarget.transform.position - m_owner.transform.position;
			//m_toTargetSquared = toTarget.sqrMagnitude;


			// #SteveD >>> pursue target
			m_owner.AgentController.Rotate(1.0f);

		}
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalAchieved()
	{
		return m_toTargetSquared <= m_successDistanceSquared;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalInvalidated()
	{
		return m_owner == null || m_cachedTarget == null;
	}

}
