using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{

	private RoomManager m_roomManager = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_roomManager = target as RoomManager;

		if (m_roomManager != null)
		{
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
			var roomsEnumerator = m_roomManager.RoomsEnumerator;

			int cachedIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel += 1;
			EditorGUI.BeginDisabledGroup(true);

			while (roomsEnumerator.MoveNext())
			{
				EditorGUILayout.Space();

				FontStyle cachedFontStyle = EditorStyles.label.fontStyle;
				EditorStyles.label.fontStyle = FontStyle.Bold;
				EditorGUI.indentLevel += 1;
				EditorGUILayout.LabelField(roomsEnumerator.Current.name);
				EditorStyles.label.fontStyle = cachedFontStyle;

				EditorGUI.indentLevel += 1;
				var inhabitantsEnumerator = roomsEnumerator.Current.InhabitantsEnumerator;
				while (inhabitantsEnumerator.MoveNext())
				{
					EditorGUILayout.TextField(inhabitantsEnumerator.Current.name);
				}
				EditorGUI.indentLevel -= 2;
			}
			
			EditorGUI.EndDisabledGroup();
			EditorGUI.indentLevel = cachedIndentLevel;
		}

		serializedObject.ApplyModifiedProperties();
	}

}