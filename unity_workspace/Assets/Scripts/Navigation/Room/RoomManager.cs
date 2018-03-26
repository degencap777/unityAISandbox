using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomManager : SingletonMonoBehaviour<RoomManager>
{

	[SerializeField, HideInInspector]
	private List<Room> m_rooms = new List<Room>();

	[SerializeField, HideInInspector]
	private List<RoomLink> m_roomLinks = new List<RoomLink>();

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		GetComponentsInChildren(m_rooms);
		GetComponentsInChildren(m_roomLinks);

		ValidateRoomLinks();

		for (int i = 0; i < m_rooms.Count; ++i)
		{
			m_rooms[i].OnAgentEnter -= OnAgentEnterRoom;
			m_rooms[i].OnAgentEnter += OnAgentEnterRoom;
			m_rooms[i].OnAgentExit -= OnAgentExitRoom;
			m_rooms[i].OnAgentExit += OnAgentExitRoom;
		}
	}

	// --------------------------------------------------------------------------------

	private void ValidateRoomLinks()
	{
		for (int i = 0; i < m_roomLinks.Count; ++i)
		{
			if (m_roomLinks[i].ConnectedLink == null)
			{
				Debug.LogErrorFormat("[RoomManager] RoomLink {0} doesn't have a connection", m_roomLinks[i].name);
			}
			else if (m_roomLinks[i].ConnectedLink.ConnectedLink != m_roomLinks[i])
			{
				Debug.LogErrorFormat("[RoomManager] RoomLinks {0} and {1} invalid", m_roomLinks[i].name, m_roomLinks[i].ConnectedLink.name);
			}
		}
	}
	
	// --------------------------------------------------------------------------------

	private void OnAgentEnterRoom(Agent agent, Room room)
	{
#if UNITY_EDITOR
		if (OnRequestRepaint != null)
		{
			OnRequestRepaint();
		}
#endif // UNITY_EDITOR
	}

	// --------------------------------------------------------------------------------

	private void OnAgentExitRoom(Agent agent, Room room)
	{
#if UNITY_EDITOR
		if (OnRequestRepaint != null)
		{
			OnRequestRepaint();
		}
#endif // UNITY_EDITOR
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public delegate void RequestRepaint();
	public event RequestRepaint OnRequestRepaint;

	// --------------------------------------------------------------------------------

	public List<Room>.Enumerator RoomEnumerator { get { return m_rooms.GetEnumerator(); } }

	// --------------------------------------------------------------------------------

	protected virtual void OnDrawGizmosSelected()
	{
		for (int i = 0; i < m_rooms.Count; ++i)
		{
			m_rooms[i].DoDrawGizmos();
		}
	}

#endif // UNITY_EDITOR

}
