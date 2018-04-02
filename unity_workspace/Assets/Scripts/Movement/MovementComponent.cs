using AISandbox.Component;
using UnityEngine;

namespace AISandbox.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class MovementComponent : BaseComponent
	{

		[SerializeField]
		private MovementConfig m_config = null;

		private Vector3 m_movementStep = Vector3.zero;
		private Vector3 m_lastMovementStep = Vector3.zero;
		private Vector3 m_appliedMovementStep = Vector3.zero;
		private float m_movementAcceleration = 0.0f;

		private float m_rotationStep = 0.0f;
		private float m_lastRotationStep = 0.0f;
		private float m_appliedRotationStep = 0.0f;
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
			if (m_config == null)
			{
				m_config = ScriptableObject.CreateInstance<MovementConfig>();
			}

			m_transform = GetComponent<Transform>();
			Debug.Assert(m_transform != null, "[MovementComponent::Awake] GetComponent<Transform> failed\n");

			m_characterController = GetComponent<CharacterController>();
			Debug.Assert(m_characterController != null, "[MovementComponent::Awake] GetComponent<CharacterController> failed\n");
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
						m_movementAcceleration = Mathf.Clamp(m_movementAcceleration + dt * m_config.MovementAccelerationFactor, 0.0f, 1.0f);
					}
					m_appliedMovementStep = m_movementStep * m_movementAcceleration;
					m_lastMovementStep = m_movementStep;
				}
				// movement deceleration
				else
				{
					if (m_movementAcceleration > 0.0f)
					{
						m_movementAcceleration = Mathf.Clamp(m_movementAcceleration - dt * m_config.MovementDecelerationFactor, 0.0f, 1.0f);
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
						m_rotationAcceleration = Mathf.Clamp(m_rotationAcceleration + dt * m_config.RotationAccelerationFactor, 0.0f, 1.0f);
					}
					m_appliedRotationStep = m_rotationStep * m_rotationAcceleration;
					m_lastRotationStep = m_rotationStep;
				}
				// rotation deceleration
				else
				{
					if (m_rotationAcceleration > 0.0f)
					{
						m_rotationAcceleration = Mathf.Clamp(m_rotationAcceleration - dt * m_config.RotationDecelerationFactor, 0.0f, 1.0f);
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

		public void MoveForward(float value)
		{
			float dt = GetScaledDeltaTime();
			if (value > 0.0f)
			{
				m_movementStep += m_transform.forward * Mathf.Clamp(value, -1.0f, 1.0f) * m_config.MoveForwardSpeed * dt;
			}
			else
			{
				m_movementStep += m_transform.forward * Mathf.Clamp(value, -1.0f, 1.0f) * m_config.MoveBackwardSpeed * dt;
			}
		}

		// --------------------------------------------------------------------------------

		public void MoveSideways(float value)
		{
			m_movementStep += m_transform.right * Mathf.Clamp(value, -1.0f, 1.0f) * m_config.MoveSidewaysSpeed * GetScaledDeltaTime();
		}

		// --------------------------------------------------------------------------------

		public void Rotate(float value)
		{
			m_rotationStep += Mathf.Clamp(value, -1.0f, 1.0f) * m_config.RotationSpeed * GetScaledDeltaTime();
		}

		// --------------------------------------------------------------------------------

		private float GetScaledDeltaTime()
		{
			return Time.deltaTime * m_deltaTimeScalar;
		}

		// Editor specific ----------------------------------------------------------------
		// --------------------------------------------------------------------------------

#if UNITY_EDITOR

		public MovementConfig Editor_Config { get { return m_config; } }

		// --------------------------------------------------------------------------------

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
}