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

	private Agent m_cachedTarget = null;
	private Transform m_cachedTargetTransform = null;
	
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
		base.OnStart();

		m_triggerDistanceSquared = m_triggerDistance * m_triggerDistance;
		m_successDistanceSquared = m_successDistance * m_successDistance;
	}

	// --------------------------------------------------------------------------------

	public override void OnEnter(WorkingMemory workingMemory)
	{
		CacheTarget();
		if (workingMemory != null)
		{
			workingMemory.OnTargetsChanged -= OnWorkingMemoryTargetsChanged;
			workingMemory.OnTargetsChanged += OnWorkingMemoryTargetsChanged;
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

		// vector away from target
		Vector3 targetToOwner = m_ownerTransform.position - m_cachedTargetTransform.position;
		m_targetToOwnerSquared = targetToOwner.sqrMagnitude;

		if (m_active || m_targetToOwnerSquared <= m_triggerDistanceSquared)
		{
			// escape angle (shortest)
			float escapeAngle = Vector3.Angle(m_ownerTransform.forward, targetToOwner);
			float absEscapeAngle = escapeAngle;
			if (Vector3.Cross(m_ownerTransform.forward, targetToOwner).y < 0)
			{
				escapeAngle = -escapeAngle;
			}

			// rotate away from target
			if (absEscapeAngle >= m_isFacingAwayAngle)
			{
				controller.Rotate(escapeAngle * m_toTurnAngleScalar);
			}

			// move away from target
			if (absEscapeAngle <= m_minimumAngleForMovement)
			{
				controller.MoveForward(1.0f);
			}

			// deactivate if reached success distance
			m_active = m_targetToOwnerSquared <= m_successDistanceSquared;
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

	private void OnWorkingMemoryTargetsChanged(WorkingMemory sender)
	{
		CacheTarget();
	}

}
