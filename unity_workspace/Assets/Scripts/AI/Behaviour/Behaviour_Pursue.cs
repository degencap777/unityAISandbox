using UnityEngine;

public class Behaviour_Pursue : Behaviour
{

	private static readonly Vector3 k_vectorUp = new Vector3(0.0f, 1.0f, 0.0f);

	// --------------------------------------------------------------------------------

	public override BehaviourId BehaviourId { get { return BehaviourId.Pursue; } }
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

	private bool m_pursuing = false;
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

	public override void OnExit()
	{
		;
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
		
		if (m_cachedTarget == null)
		{
			Debug.LogError("[Behaviour_Pursue] Unable to update >>> cached target is null\n");
			return;
		}

		if (m_cachedTargetTransform == null)
		{
			Debug.LogError("[Behaviour_Pursue] Unable to update >>> cached target transform is null\n");
			return;
		}
		
		AgentController controller = m_owner.AgentController;
		if (controller == null)
		{
			Debug.LogError("[Behaviour_Pursue] Unable to update >>> owner AgentController is null\n");
			return;
		}

		// vector to target
		Vector3 toTarget = m_cachedTargetTransform.position - m_ownerTransform.position;
		m_toTargetSquared = toTarget.sqrMagnitude;

		float angleToTarget = Vector3.SignedAngle(m_ownerTransform.forward, toTarget, k_vectorUp);
		float absAngleToTarget = Mathf.Abs(angleToTarget);

		// rotate to target
		if (absAngleToTarget >= m_isFacingAngle)
		{
			controller.Rotate(angleToTarget * m_toTurnAngleScalar);
		}

		// only move toward target if out of range
		if (m_pursuing || m_toTargetSquared >= m_triggerDistanceSquared)
		{
			// move to target
			if (absAngleToTarget <= m_minimumAngleForMovement)
			{
				controller.MoveForward(1.0f);
			}
			
			// deactivate if reached success distance
			m_pursuing = m_toTargetSquared >= m_successDistanceSquared;
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

}
