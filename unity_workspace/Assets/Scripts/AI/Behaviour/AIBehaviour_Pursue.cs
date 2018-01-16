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

	private Agent m_cachedTarget = null;
	private Transform m_cachedTargetTransform = null;

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

	public override void OnStart(Agent owner, WorkingMemory workingMemory)
	{
		base.OnStart(owner, workingMemory);

		m_triggerDistanceSquared = m_triggerDistance * m_triggerDistance;
		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter()
	{
		CacheTarget();
		if (m_workingMemory != null)
		{
			m_workingMemory.OnTargetsChanged -= OnWorkingMemoryTargetsChanged;
			m_workingMemory.OnTargetsChanged += OnWorkingMemoryTargetsChanged;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnExit()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.OnTargetsChanged -= OnWorkingMemoryTargetsChanged;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		if (m_owner == null)
		{
			Debug.LogError("[Behaviour_Pursue] Unable to update >>> owner is null\n");
			return;
		}

		if (m_ownerTransform == null)
		{
			Debug.LogError("[Behaviour_Pursue] Unable to update >>> owner transform is null\n");
			return;
		}
		
		AgentController controller = m_owner.AgentController;
		if (controller == null)
		{
			Debug.LogError("[Behaviour_Pursue] Unable to update >>> owner AgentController is null\n");
			return;
		}

		if (m_cachedTarget == null || m_cachedTargetTransform == null)
		{
			return;
		}

		// vector to target
		Vector3 toTarget = m_cachedTargetTransform.position - m_ownerTransform.position;
		m_toTargetSquared = toTarget.sqrMagnitude;

		// angle to target (shortest)
		float angleToTarget = Vector3.Angle(m_ownerTransform.forward, toTarget);
		float absAngleToTarget = angleToTarget;
		if (Vector3.Cross(m_ownerTransform.forward, toTarget).y < 0)
		{
			angleToTarget = -angleToTarget;
		}
		
		// rotate to target
		if (absAngleToTarget >= m_isFacingAngle)
		{
			controller.Rotate(angleToTarget * m_toTurnAngleScalar);
		}

		// only move toward target if out of range
		if (m_active || m_toTargetSquared >= m_triggerDistanceSquared)
		{
			// move to target
			if (absAngleToTarget <= m_minimumAngleForMovement)
			{
				controller.MoveForward(1.0f);
			}
			
			// deactivate if reached success distance
			m_active = m_toTargetSquared >= m_successDistanceSquared;
		}
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalAchieved()
	{
		return m_toTargetSquared <= m_successDistanceSquared;
	}

	// --------------------------------------------------------------------------------

	public override bool IsGoalInvalid()
	{
		return m_owner == null ||
			m_ownerTransform == null ||
			m_owner.AgentController == null ||
			m_cachedTarget == null ||
			m_cachedTargetTransform == null;
	}

	// --------------------------------------------------------------------------------

	private void CacheTarget()
	{
		if (m_workingMemory != null)
		{
			m_workingMemory.SortTargets();
			m_cachedTarget = m_workingMemory.GetHighestPriorityTarget();
			if (m_cachedTarget != null)
			{
				m_cachedTargetTransform = m_cachedTarget.transform;
			}
		}
	}

	// --------------------------------------------------------------------------------

	private void OnWorkingMemoryTargetsChanged()
	{
		CacheTarget();
	}

}
