using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HistoricMemory), true)]
public class HistoricMemoryEditor : Editor
{

	private HistoricMemory m_historicMemory = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_historicMemory = target as HistoricMemory;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		if (m_historicMemory != null)
		{
			var memories = m_historicMemory.Editor_RememberedEvents;
			if (memories != null)
			{
				float time = Time.time;

				foreach (var kvp in memories)
				{
					GUILayout.Label(kvp.Key.ToString(), EditorStyles.boldLabel);

					// cache and increase indent
					int cachedIndent = EditorGUI.indentLevel;
					EditorGUI.indentLevel += 2;

					EditorGUI.BeginDisabledGroup(true);
					for (int i = 0; i < kvp.Value.Count; ++i)
					{
						float toExpiry = Mathf.Max(0.0f, kvp.Value[i].m_expirationTime - time);
						
						// event actor
						Agent actor = kvp.Value[i].m_perceptionEvent.Actor;
						if (actor != null)
						{
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.PrefixLabel("Actor");
							EditorGUILayout.TextField(actor.name);
							EditorGUILayout.EndHorizontal();
						}

						// target
						Agent target = kvp.Value[i].m_perceptionEvent.Target;
						if (target != null)
						{
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.PrefixLabel("Target");
							EditorGUILayout.TextField(target.name);
							EditorGUILayout.EndHorizontal();
						}

						// location
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.PrefixLabel("Location");
						EditorGUILayout.TextField(kvp.Value[i].m_perceptionEvent.Location.ToString("F3"));
						EditorGUILayout.EndHorizontal();

						// expiry
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.PrefixLabel("Expires in");
						EditorGUILayout.TextField(string.Format("{0}", toExpiry));
						EditorGUILayout.EndHorizontal();
					}
					EditorGUI.EndDisabledGroup();
					
					// clear memory type
					if (kvp.Value.Count > 0)
					{
						if (GUILayout.Button("Clear"))
						{
							m_historicMemory.Editor_ClearMemoryType(kvp.Key);
						}
					}
					else
					{
						GUILayout.Label("empty");
					}

					// restore indent
					EditorGUI.indentLevel = cachedIndent;
					GUILayout.Space(6);
				}
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

	// --------------------------------------------------------------------------------

	public override bool RequiresConstantRepaint()
	{
		return true;
	}

}