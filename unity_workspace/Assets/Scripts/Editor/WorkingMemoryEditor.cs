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
			// cache current background colour
			Color cachedBackgroundColour = GUI.backgroundColor;
			float width = Screen.width - 24.0f;

			if (m_workingMemory.Editor_Targets.Count > 0)
			{
				GUILayout.Space(6);
				GUILayout.Label("Targets:", EditorStyles.boldLabel);

				float targetElementWidth = (width * 0.5f) - 6.0f;
				float targetNameWidth = targetElementWidth * 0.84f;
				float targetButtonWidth = targetElementWidth * 0.16f;

				float priorityElementWidth = (width * 0.5f) - 24.0f;
				float priorityButtonWidth = priorityElementWidth * 0.15f;
				float priorityValueWidth = priorityElementWidth * 0.4f;

				// list of all targets
				foreach (var target in m_workingMemory.Editor_Targets)
				{
					GUILayout.BeginHorizontal();
					
					// name
					EditorGUILayout.TextField(target.Agent.name, GUILayout.Width(targetNameWidth));
					
					// remove
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button("X", GUILayout.Width(targetButtonWidth)))
					{
						m_workingMemory.RemoveTarget(target.Agent);
					}
					GUI.backgroundColor = cachedBackgroundColour;

					GUILayout.Space(10);

					// reduce priority
					if (GUILayout.Button("--", GUILayout.Width(priorityButtonWidth)))
					{
						target.UpdatePriority(target.Priority - 0.1f);
					}
					if (GUILayout.Button("-", GUILayout.Width(priorityButtonWidth)))
					{
						target.UpdatePriority(target.Priority - 0.01f);
					}

					// priority
					EditorGUILayout.TextField(target.Priority.ToString(), GUILayout.Width(priorityValueWidth));

					// increase priority
					if (GUILayout.Button("+", GUILayout.Width(priorityButtonWidth)))
					{
						target.UpdatePriority(target.Priority + 0.01f);
					}
					if (GUILayout.Button("++", GUILayout.Width(priorityButtonWidth)))
					{
						target.UpdatePriority(target.Priority + 0.1f);
					}

					GUILayout.EndHorizontal();
				}

				if (GUILayout.Button("Sort targets", GUILayout.Width(width * 0.5f)))
				{
					m_workingMemory.SortTargets();
				}
			}

			if (m_workingMemory.Editor_Allies.Count > 0)
			{
				GUILayout.Space(6);
				GUILayout.Label("Allies:", EditorStyles.boldLabel);

				float allyElementWidth = (width * 0.5f) - 6.0f;
				float allyNameWidth = allyElementWidth * 0.84f;
				float allyButtonWidth = allyElementWidth * 0.16f;

				// list of all allies
				foreach (var ally in m_workingMemory.Editor_Allies)
				{
					GUILayout.BeginHorizontal();

					// name
					EditorGUILayout.TextField(ally.name, GUILayout.Width(allyNameWidth));
					
					// remove
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button("X", GUILayout.Width(allyButtonWidth)))
					{
						m_workingMemory.RemoveAlly(ally);
					}
					GUI.backgroundColor = cachedBackgroundColour;

					GUILayout.EndHorizontal();
				}
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

}