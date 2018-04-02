using UnityEngine;

namespace AISandbox.Camera
{
	public class LerpFollowCamera : FollowCamera
	{

		protected override Vector3 CalculateMovementStep()
		{
			return ((m_target.position + m_offset) - m_transform.position) * 0.1f;
		}

	}
}
