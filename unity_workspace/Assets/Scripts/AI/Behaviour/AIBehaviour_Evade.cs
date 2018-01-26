using UnityEngine;

public class AIBehaviour_Evade : AIBehaviour
{

	public override AIBehaviourId BehaviourId { get { return AIBehaviourId.Evade; } }
	public override GoalFlags AchievedGoals { get { return GoalFlags.Escape; } }
	public override GoalFlags PrerequisiteGoals { get { return GoalFlags.None; } }

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_triggerDistance = 5.0f;

	[SerializeField]
	private float m_successDistance = 6.0f;

	[SerializeField]
	private float m_minimumAngleForMovement = 45.0f;

	[SerializeField]
	private float m_isFacingAwayAngle = 5.0f;

	[SerializeField]
	private float m_toTurnAngleScalar = 0.1f;

	// --------------------------------------------------------------------------------

	private bool m_active = false;
	public bool Active { get { return m_active; } }

	private float m_triggerDistanceSquared = 0.0f;
	private float m_successDistanceSquared = 0.0f;
	private float m_targetToOwnerSquared = float.MaxValue;

	// --------------------------------------------------------------------------------

	protected override void OnValidate()
	{
		m_triggerDistanceSquared = m_triggerDistance * m_triggerDistance;
		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		m_triggerDistanceSquared = m_triggerDistance * m_triggerDistance;
		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter()
	{
		;
	}
	
	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		// validate owner
		if (Owner == null || Owner.Transform == null || Owner.AgentController == null)
		{
			return;
		}

		// validate target
		Agent target = WorkMemory.GetHighestPriorityTarget();
		if (target == null || target.Transform == null)
		{
			return;
		}
		
		// vector away from target
		Vector3 targetToOwner = Owner.Transform.position - target.Transform.position;
		m_targetToOwnerSquared = targetToOwner.sqrMagnitude;
		
		if (m_active || m_targetToOwnerSquared <= m_triggerDistanceSquared)
		{
			// escape angle (shortest)
			float escapeAngle = Vector3.Angle(Owner.Transform.forward, targetToOwner);
			float absEscapeAngle = escapeAngle;
			if (Vector3.Cross(Owner.Transform.forward, targetToOwner).y < 0)
			{
				escapeAngle = -escapeAngle;
			}
		
			// rotate away from target
			if (absEscapeAngle >= m_isFacingAwayAngle)
			{
				Owner.AgentController.Rotate(escapeAngle * m_toTurnAngleScalar);
			}
		
			// move away from target
			if (absEscapeAngle <= m_minimumAngleForMovement)
			{
				Owner.AgentController.MoveForward(1.0f);
			}
		
			// deactivate if reached success distance
			m_active = m_targetToOwnerSquared <= m_successDistanceSquared;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnExit()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void Reset()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalAchieved()
	{
		return m_targetToOwnerSquared >= m_successDistanceSquared;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalInvalid()
	{
		return false;
	}
	
}
