using UnityEngine;

[ExecuteInEditMode]
public class RoomLink : MonoBehaviour
{

	private Room m_room = null;
	public Room Room
	{
		get { return m_room; }
		set { m_room = value; }
	}

	[SerializeField]
	private RoomLink m_connectedLink = null;

	// --------------------------------------------------------------------------------

	public Room GetConnectedRoom()
	{
		return m_connectedLink != null ? m_connectedLink.Room : null;
	}

	// --------------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private static readonly Vector3 k_connectionIconOffset = new Vector3(0.0f, 0.15f, 0.0f);
	private static readonly Color k_connectionColour = new Color(0.0f, 0.25f, 1.0f, 1.0f);

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		if (m_connectedLink != null)
		{
			Color cachedColour = Gizmos.color;
			Gizmos.color = k_connectionColour;
			Gizmos.DrawLine(transform.position, m_connectedLink.transform.position);

			Vector3 distance = transform.position - m_connectedLink.transform.position;
			Vector3 centre = transform.position - (distance * 0.5f);

			Gizmos.DrawIcon(centre + k_connectionIconOffset, "room_connected.png", true);
		}
		else
		{
			Gizmos.DrawIcon(transform.position + k_connectionIconOffset, "room_disconnect.png", true);
		}
	}

#endif // UNITY_EDITOR

}
