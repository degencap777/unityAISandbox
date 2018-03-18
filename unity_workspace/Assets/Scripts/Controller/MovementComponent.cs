using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementComponent : BaseComponent
{

	// movement
	[SerializeField]
	private float m_moveForwardSpeed = 8.5f;
	[SerializeField]
	private float m_moveBackwardSpeed = 7.0f;
	[SerializeField]
	private float m_moveSidewaysSpeed = 7.0f;
	private Vector3 m_movementStep = Vector3.zero;
	private Vector3 m_lastMovementStep = Vector3.zero;
	private Vector3 m_appliedMovementStep = Vector3.zero;

	// rotation
	[SerializeField]
	private float m_rotationSpeed = 360.0f;
	private float m_rotationStep = 0.0f;
	private float m_lastRotationStep = 0.0f;
	private float m_appliedRotationStep = 0.0f;

	// movement acceleration, deceleration
	[SerializeField]
	private float m_movementAccelerationFactor = 3.0f;
	[SerializeField]
	private float m_movementDecelerationFactor = 3.0f;
	private float m_movementAcceleration = 0.0f;

	// rotation acceleration, deceleration
	[SerializeField]
	private float m_rotationAccelerationFactor = 100.0f;
	[SerializeField]
	private float m_rotationDecelerationFactor = 100.0f;
	private float m_rotationAcceleration = 0.0f;
	
	// --------------------------------------------------------------------------------

	[Header("Debug utility")]

	[SerializeField]
	private bool m_movementFrozen = false;

	[SerializeField, Range(0.01f, 2.5f)]
	private float m_deltaTimeScalar = 1.0f;

	// --------------------------------------------------------------------------------

	// cached variables
	private Transform m_transform = null;
	private CharacterController m_characterController = null;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		m_transform = GetComponent<Transform>();
		Debug.Assert(m_transform != null, "[MovementComponent::Awake] GetComponent<Transform> failed\n");

		m_characterController = GetComponent<CharacterController>();
		Debug.Assert(m_characterController != null, "[MovementComponent::Awake] GetComponent<CharacterController> failed\n");
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		if (m_characterController != null)
		{
			float dt = GetScaledDeltaTime();

			// movement acceleration
			if (m_movementStep.sqrMagnitude > 0.0f)
			{
				if (m_movementAcceleration < 1.0f)
				{
					m_movementAcceleration = Mathf.Clamp(m_movementAcceleration + dt * m_movementAccelerationFactor, 0.0f, 1.0f);
				}
				m_appliedMovementStep = m_movementStep * m_movementAcceleration;
				m_lastMovementStep = m_movementStep;
			}
			// movement deceleration
			else
			{
				if (m_movementAcceleration > 0.0f)
				{
					m_movementAcceleration = Mathf.Clamp(m_movementAcceleration - dt * m_movementDecelerationFactor, 0.0f, 1.0f);
					m_appliedMovementStep = m_lastMovementStep * m_movementAcceleration;
				}
			}

			// move
			if (m_movementFrozen == false)
			{
				m_characterController.Move(m_appliedMovementStep);
			}
			m_movementStep.Set(0.0f, 0.0f, 0.0f);

			// rotation acceleration
			if (m_rotationStep != 0.0f)
			{
				if (m_rotationAcceleration < 1.0f)
				{
					m_rotationAcceleration = Mathf.Clamp(m_rotationAcceleration + dt * m_rotationAccelerationFactor, 0.0f, 1.0f);
				}
				m_appliedRotationStep = m_rotationStep * m_rotationAcceleration;
				m_lastRotationStep = m_rotationStep;
			}
			// rotation deceleration
			else
			{
				if (m_rotationAcceleration > 0.0f)
				{
					m_rotationAcceleration = Mathf.Clamp(m_rotationAcceleration - dt * m_rotationDecelerationFactor, 0.0f, 1.0f);
					m_appliedRotationStep = m_lastRotationStep * m_rotationAcceleration;
				}
			}

			// rotate
			if (m_movementFrozen == false)
			{
				m_transform.Rotate(0.0f, m_appliedRotationStep, 0.0f);
			}
			m_rotationStep = 0.0f;
		}
	}

	// --------------------------------------------------------------------------------

	public override void Destroy()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void MoveForward(float value)
	{
		float dt = GetScaledDeltaTime();
		if (value > 0.0f)
		{
			m_movementStep += m_transform.forward * Mathf.Clamp(value, -1.0f, 1.0f) * m_moveForwardSpeed * dt;
		}
		else
		{
			m_movementStep += m_transform.forward * Mathf.Clamp(value, -1.0f, 1.0f) * m_moveBackwardSpeed * dt;
		}
	}

	// --------------------------------------------------------------------------------

	public void MoveSideways(float value)
	{
		m_movementStep += m_transform.right * Mathf.Clamp(value, -1.0f, 1.0f) * m_moveSidewaysSpeed * GetScaledDeltaTime();
	}

	// --------------------------------------------------------------------------------

	public void Rotate(float value)
	{
		m_rotationStep += Mathf.Clamp(value, -1.0f, 1.0f) * m_rotationSpeed * GetScaledDeltaTime();
	}

	// --------------------------------------------------------------------------------

	private float GetScaledDeltaTime()
	{
		return Time.deltaTime* m_deltaTimeScalar;
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	protected virtual void OnDrawGizmos()
	{
		if (m_transform == null)
		{
			return;
		}

		Color originalColor = Gizmos.color;
		Vector3 origin = m_transform.position;
		origin.y += 1.0f;

		// forward, z
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(origin, origin + m_transform.forward);

		// right, x
		Gizmos.color = Color.red;
		Gizmos.DrawLine(origin, origin + m_transform.right);

		// up, y
		//Gizmos.color = Color.green;
		//Gizmos.DrawLine(origin, origin + m_transform.up);

		Gizmos.color = originalColor;
	}

	// --------------------------------------------------------------------------------

	public void Editor_Freeze(bool frozen)
	{
		m_movementFrozen = frozen;
	}

#endif // UNITY_EDITOR

}
