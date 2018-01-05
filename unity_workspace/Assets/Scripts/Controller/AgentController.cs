using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AgentController : MonoBehaviour
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

	// cached variables
	private Transform m_transform = null;
	private CharacterController m_characterController = null;

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_transform = GetComponent<Transform>();
		m_characterController = GetComponent<CharacterController>();
	}

	// --------------------------------------------------------------------------------

	public void OnUpdate()
	{
		if (m_characterController != null)
		{
			// movement acceleration
			if (m_movementStep.sqrMagnitude > 0.0f)
			{
				if (m_movementAcceleration < 1.0f)
				{
					m_movementAcceleration = Mathf.Clamp(m_movementAcceleration + Time.deltaTime * m_movementAccelerationFactor, 0.0f, 1.0f);
				}
				m_appliedMovementStep = m_movementStep * m_movementAcceleration;
				m_lastMovementStep = m_movementStep;
			}
			// movement deceleration
			else
			{
				if (m_movementAcceleration > 0.0f)
				{
					m_movementAcceleration = Mathf.Clamp(m_movementAcceleration - Time.deltaTime * m_movementDecelerationFactor, 0.0f, 1.0f);
					m_appliedMovementStep = m_lastMovementStep * m_movementAcceleration;
				}
			}

			// move
			m_characterController.Move(m_appliedMovementStep);
			m_movementStep.Set(0.0f, 0.0f, 0.0f);

			// rotation acceleration
			if (m_rotationStep != 0.0f)
			{
				if (m_rotationAcceleration < 1.0f)
				{
					m_rotationAcceleration = Mathf.Clamp(m_rotationAcceleration + Time.deltaTime * m_rotationAccelerationFactor, 0.0f, 1.0f);
				}
				m_appliedRotationStep = m_rotationStep * m_rotationAcceleration;
				m_lastRotationStep = m_rotationStep;
			}
			// rotation deceleration
			else
			{
				if (m_rotationAcceleration > 0.0f)
				{
					m_rotationAcceleration = Mathf.Clamp(m_rotationAcceleration - Time.deltaTime * m_rotationDecelerationFactor, 0.0f, 1.0f);
					m_appliedRotationStep = m_lastRotationStep * m_rotationAcceleration;
				}
			}

			// rotate
			m_transform.Rotate(0.0f, m_appliedRotationStep, 0.0f);
			m_rotationStep = 0.0f;
		}
	}

	// --------------------------------------------------------------------------------

	public void MoveForward(float value)
	{
		if (value > 0.0f)
		{
			m_movementStep += m_transform.forward * value * m_moveForwardSpeed * Time.deltaTime;
		}
		else
		{
			m_movementStep += m_transform.forward * value * m_moveBackwardSpeed * Time.deltaTime;
		}
	}

	// --------------------------------------------------------------------------------

	public void MoveSideways(float value)
	{
		m_movementStep += m_transform.right * value * m_moveSidewaysSpeed * Time.deltaTime;
	}

	// --------------------------------------------------------------------------------

	public void Rotate(float value)
	{
		m_rotationStep += value * m_rotationSpeed * Time.deltaTime;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		if (m_transform == null)
		{
			return;
		}

		Color originalColor = Gizmos.color;

		// forward, z
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.forward * 2.0f);

		// right, x
		Gizmos.color = Color.red;
		Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.right * 2.0f);

		// up, y
		Gizmos.color = Color.green;
		Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.up * 2.0f);

		Gizmos.color = originalColor;
	}

}
