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

	// #SteveD >>>	change sphere to sprite (red X for invalid connection, connection icon for valid connection
	protected virtual void OnDrawGizmos()
	{
		Color cachedColour = Gizmos.color;

		if (m_connectedLink != null)
		{
			Color colour = Color.green;
			colour.a = 0.5f; 
			Gizmos.color = colour;

			Gizmos.DrawLine(transform.position, m_connectedLink.transform.position);
			Gizmos.DrawSphere(transform.position, 0.25f);
		}
		else
		{
			Color colour = Color.red;
			colour.a = 0.5f;
			Gizmos.color = colour;

			Gizmos.DrawSphere(transform.position, 0.25f);
		}

		Gizmos.color = cachedColour;
	}

#endif // UNITY_EDITOR

}
