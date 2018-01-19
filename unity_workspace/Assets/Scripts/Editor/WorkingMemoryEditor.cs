using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorkingMemory), true)]
public class WorkingMemoryEditor : Editor
{

	private WorkingMemory m_workingMemory = null;
	
	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_workingMemory = target as WorkingMemory;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		if (m_workingMemory != null)
		{
			if (m_workingMemory.Editor_Targets.Count > 0)
			{
				GUILayout.Space(6);
				GUILayout.Label("Targets:", EditorStyles.boldLabel);

				// create a label style for behaviours
				GUIStyle targetTextFieldStyle = new GUIStyle(GUI.skin.textField)
				{
					fontStyle = FontStyle.Italic,
				};

				// list of all targets
				foreach (var target in m_workingMemory.Editor_Targets)
				{
					GUILayout.BeginHorizontal();
					EditorGUILayout.TextField(target.Target.name, targetTextFieldStyle);
					GUILayout.Space(12);
					EditorGUILayout.TextField(target.Priority.ToString(), targetTextFieldStyle);
					GUILayout.EndHorizontal();
				}
			}

			if (m_workingMemory.Editor_Allies.Count > 0)
			{
				GUILayout.Space(6);
				GUILayout.Label("Allies:", EditorStyles.boldLabel);

				GUIStyle allyTextFieldStyle = new GUIStyle(GUI.skin.textField)
				{
					fontStyle = FontStyle.Italic,
				};

				// list of all allies
				foreach (var ally in m_workingMemory.Editor_Allies)
				{
					EditorGUILayout.TextField(ally.name, allyTextFieldStyle);
				}
			}

			GUILayout.Space(6);

			// disable buttons if we're in not playing
			EditorGUI.BeginDisabledGroup(Application.isPlaying == false);
			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Clear Targets"))
			{
				m_workingMemory.Editor_ClearTargets();
			}
			if (GUILayout.Button("Clear Allies"))
			{
				m_workingMemory.Editor_ClearAllies ();
			}

			GUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();
		}

		serializedObject.ApplyModifiedProperties();
	}

}