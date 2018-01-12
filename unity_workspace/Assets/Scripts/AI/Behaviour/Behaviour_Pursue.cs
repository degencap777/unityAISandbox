using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Pursue : Behaviour
{

	public override Goal AchievesGoal { get { return Goal.CloseDown; } }

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_successDistance = 2.0f;
	private float m_successDistanceSquared = 4.0f;

	private float m_toTargetSquared = float.MaxValue;

	// --------------------------------------------------------------------------------

	// cache
	private Agent m_cachedTarget = null;

	// --------------------------------------------------------------------------------

	public Behaviour_Pursue(Agent owner, Memory memory)
		: base(owner, memory)
	{
		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter()
	{
		if (m_memory != null && m_memory.WorkingMemory != null)
		{
			m_memory.WorkingMemory.SortTargets();

			m_cachedTarget = m_memory.WorkingMemory.GetHighestPriorityTarget();
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
