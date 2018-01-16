using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIDiagnosticCommand), true)]
public class AIDiagnosticCommandEditor : Editor
{

	private AIDiagnosticCommand m_command = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_command = target as AIDiagnosticCommand;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		if (m_command != null && GUILayout.Button("Execute"))
		{
			m_command.EditorExecute();
		}

		serializedObject.ApplyModifiedProperties();
	}

}