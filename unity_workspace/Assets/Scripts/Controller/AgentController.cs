using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AgentController : MonoBehaviour
{

	[SerializeField]
	private CharacterController m_characterController = null;

	// --------------------------------------------------------------------------------

	[SerializeField]
	private float m_moveForwardSpeed = 10.0f;
	[SerializeField]
	private float m_moveBackwardSpeed = 7.0f;
	[SerializeField]
	private float m_moveSidewardSpeed = 6.0f;
	[SerializeField]
	private float m_rotateSpeed = 360.0f;

	// --------------------------------------------------------------------------------

	// #SteveD >>> Todo >>> movement & rotation to be applied at end of update

	// --------------------------------------------------------------------------------

	public void MoveForward(float value)
	{
		// #SteveD >>> Todo >>> Cache all movement and rotation and apply at end of update
	}

	// --------------------------------------------------------------------------------

	public void MoveSideward(float value)
	{
		// #SteveD >>> Todo >>> Cache all movement and rotation and apply at end of update
	}

	// --------------------------------------------------------------------------------

	public void Rotate(float value)
	{
		// #SteveD >>> Todo >>> Cache all movement and rotation and apply at end of update
	}

}
