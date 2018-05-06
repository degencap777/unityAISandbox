using AISandbox.Navigation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavMesh), true)]
public class NavMeshEditor : Editor
{

	private NavMesh m_navMesh = null;
	private SerializedProperty m_dataContainerProperty = null;
	
	private SerializedProperty m_nodeColourProperty = null;
	private SerializedProperty m_edgeColourProperty = null;
	
	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_navMesh = target as NavMesh;
		m_dataContainerProperty = serializedObject.FindProperty("m_dataContainer");
		m_nodeColourProperty = serializedObject.FindProperty("m_nodeColour");
		m_edgeColourProperty = serializedObject.FindProperty("m_edgeColour");
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		
		if (m_navMesh != null)
		{
			if (m_dataContainerProperty != null)
			{
				EditorGUILayout.PropertyField(m_dataContainerProperty);
			}
			
			// persistence
			GUILayout.Space(12);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Save"))
			{
				m_navMesh.Editor_WriteToAsset();
				EditorUtility.SetDirty(m_dataContainerProperty.objectReferenceValue);
				AssetDatabase.SaveAssets();
			}
			if (GUILayout.Button("Load"))
			{
				m_navMesh.Editor_ReadFromAsset();
			}
			GUILayout.EndHorizontal();

			// debug colours
			GUILayout.Space(6);
			float cachedLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 64.0f;
			GUILayout.BeginHorizontal();
			DrawDebugColour(m_nodeColourProperty, "Node");
			DrawDebugColour(m_edgeColourProperty, "Edge");
			EditorGUIUtility.labelWidth = cachedLabelWidth;
			GUILayout.EndHorizontal();
		}

		serializedObject.ApplyModifiedProperties();
	}

	// --------------------------------------------------------------------------------

	private void DrawDebugColour(SerializedProperty property, string label)
	{
		GUILayout.BeginVertical();
		EditorGUILayout.LabelField(string.Format("{0} colour", label));
		property.colorValue = EditorGUILayout.ColorField(property.colorValue);
		GUILayout.EndVertical();
	}

}