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
	}

	// --------------------------------------------------------------------------------

	protected virtual Vector3 CalculateMovementStep()
	{
		return (m_target.position + m_offset) - m_transform.position;
	}

	// --------------------------------------------------------------------------------

	private void Update()
	{
		if (m_transform != null)
		{
			m_transform.Translate(CalculateMovementStep(), Space.World);
		}
	}

}
