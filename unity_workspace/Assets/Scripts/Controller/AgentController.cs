using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AgentController : MonoBehaviour
{
	
	[SerializeField]
	private float m_moveForwardSpeed = 10.0f;
	[SerializeField]
	private float m_moveBackwardSpeed = 7.0f;
	[SerializeField]
	private float m_moveSidewaysSpeed = 6.0f;
	[SerializeField]
	private float m_rotateSpeed = 360.0f;
	
	private Vector3 m_movementStep = Vector3.zero;
	private float m_rotationStep = 0.0f;

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

	protected virtual void Update()
	{
		if (m_characterController != null)
		{
			m_characterController.Move(m_movementStep);
			m_movementStep.Set(0.0f, 0.0f, 0.0f);

			m_transform.Rotate(0.0f, m_rotationStep, 0.0f);
			m_rotationStep = 0.0f;
		}
	}

	// --------------------------------------------------------------------------------

	public void MoveForward(float value)
	{
		if (value > 0.0f)
		{
			m_movementStep.z += value * m_moveForwardSpeed * Time.deltaTime;
		}
		else
		{
			m_movementStep.z += value * m_moveBackwardSpeed * Time.deltaTime;
		}
	}

	// --------------------------------------------------------------------------------

	public void MoveSideways(float value)
	{
		m_movementStep.x += value * m_moveSidewaysSpeed * Time.deltaTime;
	}

	// --------------------------------------------------------------------------------

	public void Rotate(float value)
	{
		m_rotationStep += value * m_rotateSpeed * Time.deltaTime;
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
