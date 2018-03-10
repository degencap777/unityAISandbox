using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomLink))]
public class RoomLinkEditor : Editor
{

	private RoomLink m_roomLink = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_roomLink = target as RoomLink;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();
		EditorGUILayout.Space();

		if (m_roomLink != null && m_roomLink.ConnectedLink != null)
		{
			if (m_roomLink.ConnectedLink.ConnectedLink == m_roomLink)
			{
				if (GUILayout.Button("Sever connection"))
				{
					m_roomLink.Editor_SeverConnection();
					Repaint();
				}
			}
			else
			{
				if (GUILayout.Button("Reciprocate connection"))
				{
					m_roomLink.Editor_ReciprocateConnection();
					Repaint();
				}
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

}