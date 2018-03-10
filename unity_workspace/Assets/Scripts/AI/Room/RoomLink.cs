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
	public RoomLink ConnectedLink { get { return m_connectedLink; } }

	// --------------------------------------------------------------------------------

	public Room GetConnectedRoom()
	{
		return m_connectedLink != null ? m_connectedLink.Room : null;
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	private static readonly Vector3 k_connectionIconOffset = new Vector3(0.0f, 0.15f, 0.0f);

	// --------------------------------------------------------------------------------

	public void Editor_ReciprocateConnection()
	{
		if (m_connectedLink != null)
		{
			m_connectedLink.m_connectedLink = this;
		}
	}

	// --------------------------------------------------------------------------------

	public void Editor_SeverConnection()
	{
		if (m_connectedLink != null)
		{
			m_connectedLink.m_connectedLink = null;
			m_connectedLink = null;
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmos()
	{
		if (m_connectedLink != null)
		{
			Vector3 distance = transform.position - m_connectedLink.transform.position;
			Vector3 centre = transform.position - (distance * 0.5f);
			
			if (m_connectedLink.m_connectedLink == null)
			{
				Gizmos.DrawIcon(centre + k_connectionIconOffset, "room_one_way_connection.png", true);
			}
			else 
			{ 
				Gizmos.DrawIcon(centre + k_connectionIconOffset, "room_connected.png", true); 
			}
		}
		else
		{
			Gizmos.DrawIcon(transform.position + k_connectionIconOffset, "room_disconnect.png", true);
		}
	}

#endif // UNITY_EDITOR

}
