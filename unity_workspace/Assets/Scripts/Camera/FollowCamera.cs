using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

	[SerializeField]
	protected Transform m_target = null;

	[SerializeField]
	protected Vector3 m_offset = new Vector3(0.0f, 10.0f, 0.0f);

	// --------------------------------------------------------------------------------

	protected Transform m_transform = null;

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_transform = GetComponent<Transform>();
		Debug.Assert(m_transform != null, "[FollowCamera::Awake] GetComponent<Transform> failed\n");
	}

	// --------------------------------------------------------------------------------

	private void Update()
	{
		if (m_transform != null)
		{
			Vector3 step = CalculateMovementStep();
			if (step.sqrMagnitude > float.Epsilon)
			{
				m_transform.Translate(step, Space.World);
			}
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual Vector3 CalculateMovementStep()
	{
		return (m_target.position + m_offset) - m_transform.position;
	}

}
