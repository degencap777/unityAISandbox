using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test), true)]
public class TestEditor : Editor
{

	private Test m_test = null;
	
	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_test = target as Test;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		if (m_test != null && GUILayout.Button("RunTest"))
		{
			m_test.EditorRunAndReset();
		}
		
		DrawDefaultInspector();
		serializedObject.ApplyModifiedProperties();
	}

}