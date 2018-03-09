using System.Collections.Generic;
using UnityEngine;

class RoomManager : SingletonMonoBehaviour<RoomManager>
{

	// #SteveD	>>> custom editor showing all rooms & their inhabitants

	// #SteveD	>>> links between rooms at link location so we can estimate 
	//				distance between links/rooms

	[SerializeField, HideInInspector]
	private List<Room> m_rooms = new List<Room>();

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		GetComponentsInChildren(m_rooms);
	}

}
