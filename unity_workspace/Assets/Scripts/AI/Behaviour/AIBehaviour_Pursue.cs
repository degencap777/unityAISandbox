using UnityEngine;

public class AIBehaviour_Pursue : AIBehaviour
{

	public override AIBehaviourId BehaviourId { get { return AIBehaviourId.Pursue; } }
	public override GoalFlags AchievedGoals { get { return GoalFlags.CloseDown; } }
	public override GoalFlags PrerequisiteGoals { get { return GoalFlags.None; } }

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_triggerDistance = 5.0f;

	[SerializeField]
	private float m_successDistance = 4.0f;

	[SerializeField]
	private float m_minimumAngleForMovement = 30.0f;

	[SerializeField]
	private float m_isFacingAngle = 5.0f;

	[SerializeField]
	private float m_toTurnAngleScalar = 0.1f;

	// --------------------------------------------------------------------------------

	private bool m_active = false;
	public bool Active { get { return m_active; } }

	private float m_successDistanceSquared = 0.0f;
	private float m_triggerDistanceSquared = 0.0f;
	private float m_toTargetSquared = float.MaxValue;

	// --------------------------------------------------------------------------------

	protected override void OnValidate()
	{
		m_triggerDistanceSquared = m_triggerDistance * m_triggerDistance;
		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
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
		/// validate owner
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
				
		// vector to target
		Vector3 toTarget = target.Transform.position - Owner.Transform.position;
		m_toTargetSquared = toTarget.sqrMagnitude;
		
		// angle to target (shortest)
		float angleToTarget = Vector3.Angle(Owner.Transform.forward, toTarget);
		float absAngleToTarget = angleToTarget;
		if (Vector3.Cross(Owner.Transform.forward, toTarget).y < 0)
		{
			angleToTarget = -angleToTarget;
		}
		
		// rotate to target
		if (absAngleToTarget >= m_isFacingAngle)
		{
			Owner.AgentController.Rotate(angleToTarget * m_toTurnAngleScalar);
		}
		
		// only move toward target if out of range
		if (m_active || m_toTargetSquared >= m_triggerDistanceSquared)
		{
			// move to target
			if (absAngleToTarget <= m_minimumAngleForMovement)
			{
				Owner.AgentController.MoveForward(1.0f);
			}
			
			// deactivate if reached success distance
			m_active = m_toTargetSquared >= m_successDistanceSquared;
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
		return m_toTargetSquared <= m_successDistanceSquared;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalInvalid()
	{
		return false;
	}

}
