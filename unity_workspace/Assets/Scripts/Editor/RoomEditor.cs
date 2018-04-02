using AISandbox.Navigation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
[CanEditMultipleObjects]
public class RoomEditor : Editor
{

	private Room m_room = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_room = target as Room;

		if (m_room != null)
		{
			m_room.OnRequestRepaint -= Repaint;
			m_room.OnRequestRepaint += Repaint;
		}
	}

	// --------------------------------------------------------------------------------

	public void OnDisable()
	{
		if (m_room != null)
		{
			m_room.OnRequestRepaint -= Repaint;
		}
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();
		EditorGUILayout.Space();

		if (m_room != null)
		{
			FontStyle cachedFontStyle = EditorStyles.label.fontStyle;
			EditorStyles.label.fontStyle = FontStyle.Bold;
			EditorGUILayout.LabelField("Inhabitants");
			EditorStyles.label.fontStyle = cachedFontStyle;

			int cachedIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel += 1;
			EditorGUI.BeginDisabledGroup(true);
			var inhabitantsEnumerator = m_room.InhabitantsEnumerator;
			while (inhabitantsEnumerator.MoveNext())
			{
				EditorGUILayout.TextField(inhabitantsEnumerator.Current.name);
			}
			EditorGUI.EndDisabledGroup();
			EditorGUI.indentLevel = cachedIndentLevel;
		}

		serializedObject.ApplyModifiedProperties();
	}

}