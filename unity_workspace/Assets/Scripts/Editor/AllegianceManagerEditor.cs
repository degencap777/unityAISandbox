using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AllegianceManager))]
public class AllegianceManagerEditor : Editor
{

	private AllegianceManager m_allegianceManager = null;
	private SerializedProperty m_allegiances = null;
	private List<int> m_toRemove = new List<int>();

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_allegianceManager = target as AllegianceManager;
		m_allegiances = serializedObject.FindProperty("m_allegiances");
	}
	
	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		if (m_allegiances != null && m_allegianceManager != null)
		{
			int cachedIndentLevel = EditorGUI.indentLevel;

			for (int i = 0; i < m_allegiances.arraySize; ++i)
			{
				EditorGUI.indentLevel += 1;
				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(m_allegiances.GetArrayElementAtIndex(i));

				if (GUILayout.Button("X", GUILayout.MaxWidth(24.0f)))
				{
					m_toRemove.Add(m_allegiances.GetArrayElementAtIndex(i).FindPropertyRelative("m_id").intValue);
				}

				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel -= 1;
			}

			serializedObject.ApplyModifiedProperties();

			for (int i = 0; i < m_toRemove.Count; ++i)
			{
				m_allegianceManager.Editor_RemoveAllegiance(m_toRemove[i]);
			}
			
			if (GUILayout.Button("+"))
			{
				m_allegianceManager.Editor_CreateAllegiance();
			}

			EditorGUI.indentLevel = cachedIndentLevel;
		}
	}

}