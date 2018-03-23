using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Component Settings/Movement", order = 1)]
public class MovementSettings : ScriptableObject
{

	[SerializeField]
	private float m_moveForwardSpeed = 8.5f;
	public float MoveForwardSpeed { get { return m_moveForwardSpeed; } }

	[SerializeField]
	private float m_moveBackwardSpeed = 7.0f;
	public float MoveBackwardSpeed { get { return m_moveBackwardSpeed; } }

	[SerializeField]
	private float m_moveSidewaysSpeed = 7.0f;
	public float MoveSidewaysSpeed { get { return m_moveSidewaysSpeed; } }

	[SerializeField]
	private float m_movementAccelerationFactor = 3.0f;
	public float MovementAccelerationFactor { get { return m_movementAccelerationFactor; } }

	[SerializeField]
	private float m_movementDecelerationFactor = 3.0f;
	public float MovementDecelerationFactor { get { return m_movementDecelerationFactor; } }
	
	[SerializeField]
	private float m_rotationSpeed = 360.0f;
	public float RotationSpeed { get { return m_rotationSpeed; } }

	[SerializeField]
	private float m_rotationAccelerationFactor = 100.0f;
	public float RotationAccelerationFactor { get { return m_rotationAccelerationFactor; } }

	[SerializeField]
	private float m_rotationDecelerationFactor = 100.0f;
	public float RotationDecelerationFactor { get { return m_rotationDecelerationFactor; } }

}