using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavMesh), true)]
public class NavMeshEditor : Editor
{

	private NavMesh m_navMesh = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_navMesh = target as NavMesh;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		if (m_navMesh != null && GUILayout.Button("Generate Grid"))
		{
			m_navMesh.Editor_GenerateUniformGraph();
		}

		serializedObject.ApplyModifiedProperties();
	}

}