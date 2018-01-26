using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIBehaviourController), true)]
public class BehaviourControllerEditor : Editor
{

	private AIBehaviourController m_collection = null;
	
	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_collection = target as AIBehaviourController;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		if (m_collection != null)
		{
			float elementWidth = (Screen.width * 0.5f) - 12.0f;

			// cache current behaviour Id
			AIBehaviourId activeBehaviourId = AIBehaviourId.None;
			if (m_collection.Editor_CurrentBehaviour != null)
			{
				activeBehaviourId = m_collection.Editor_CurrentBehaviour.BehaviourId;
			}

			if (m_collection.Editor_Behaviours.Count > 0)
			{
				GUILayout.Space(6);
				GUILayout.Label("Behaviours:", EditorStyles.boldLabel);

				// cache current background colour
				Color cachedColor = GUI.backgroundColor;
				
				// list of all behaviours
				foreach (var behaviour in m_collection.Editor_Behaviours.Values)
				{
					// set content colour if this is our active behaviour
					if (behaviour.BehaviourId == activeBehaviourId)
					{
						GUI.backgroundColor = Color.green;
					}
					EditorGUILayout.TextField(behaviour.BehaviourId.ToString(), GUILayout.Width(elementWidth));
					
					// reset background colour
					GUI.backgroundColor = cachedColor;
				}
			}

			GUILayout.Space(6);

			// disable buttons if we're in not playing
			EditorGUI.BeginDisabledGroup(Application.isPlaying == false);
			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Reset Current Behaviour", GUILayout.Width(elementWidth)))
			{
				m_collection.Editor_ResetCurrentBehaviour();
			}
			if (GUILayout.Button("Hard Reset", GUILayout.Width(elementWidth)))
			{
				m_collection.Editor_ResetCollection();
			}

			GUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();
		}

		serializedObject.ApplyModifiedProperties();
	}

}