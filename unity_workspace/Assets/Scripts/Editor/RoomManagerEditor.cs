using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{

	private RoomManager m_roomManager = null;
	private List<bool> m_unfoldedRooms = new List<bool>();

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_roomManager = target as RoomManager;
		
		if (m_roomManager != null)
		{
			m_unfoldedRooms.Clear();
			var roomEnumerator = m_roomManager.RoomEnumerator;
			while (roomEnumerator.MoveNext())
			{
				m_unfoldedRooms.Add(false);
			}

			m_roomManager.OnRequestRepaint -= Repaint;
			m_roomManager.OnRequestRepaint += Repaint;
		}
	}

	// --------------------------------------------------------------------------------

	public void OnDisable()
	{
		if (m_roomManager != null)
		{
			m_roomManager.OnRequestRepaint -= Repaint;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();
		
		if (m_roomManager != null)
		{
			int cachedIndentLevel = EditorGUI.indentLevel;
			
			var roomsEnumerator = m_roomManager.RoomEnumerator;
			int roomIndex = 0;
			while (roomsEnumerator.MoveNext())
			{
				m_unfoldedRooms[roomIndex] = EditorGUILayout.Foldout(m_unfoldedRooms[roomIndex], roomsEnumerator.Current.name);
				if (m_unfoldedRooms[roomIndex])
				{
					EditorGUI.BeginDisabledGroup(true);
					
					EditorGUI.indentLevel += 1;
					EditorGUILayout.LabelField("Inhabitants");
					EditorGUI.indentLevel += 1;
					
					var inhabitantsEnumerator = roomsEnumerator.Current.InhabitantsEnumerator;
					while (inhabitantsEnumerator.MoveNext())
					{
						EditorGUILayout.TextField(inhabitantsEnumerator.Current.name);
					}

					EditorGUI.indentLevel -= 1;
					EditorGUILayout.LabelField("Links");
					EditorGUI.indentLevel += 1;

					var linksEnumerator = roomsEnumerator.Current.LinksEnumerator;
					while (linksEnumerator.MoveNext())
					{
						RoomLink link = linksEnumerator.Current;
						RoomLink other = link.ConnectedLink;

						if (link == null || link.Room == null)
						{
							EditorGUILayout.TextField("X <-> X");
						}
						else if (other == null || other.Room == null)
						{
							EditorGUILayout.TextField(string.Format("{0} <-> X", link.Room.name));
						}
						else
						{
							EditorGUILayout.LabelField(string.Format("{0} <-> {1}", link.Room.name, other.Room.name));
						}
					}
					EditorGUI.indentLevel -= 2;
				}
				EditorGUI.EndDisabledGroup();
				++roomIndex;
			}
			
			EditorGUI.indentLevel = cachedIndentLevel;
		}

		serializedObject.ApplyModifiedProperties();
	}

}