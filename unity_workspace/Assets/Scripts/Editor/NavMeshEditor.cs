using AISandbox.Navigation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavMesh), true)]
public class NavMeshEditor : Editor
{

	private NavMesh m_navMesh = null;
	private SerializedProperty m_boundsProperty = null;
	private SerializedProperty m_dataContainerProperty = null;
	//private SerializedProperty m_cellDimensionProperty = null;
	private bool m_enableEdit = false;
	private SerializedProperty m_editorNodePrototypeProperty = null;

	private SerializedProperty m_nodeColourProperty = null;
	private SerializedProperty m_edgeColourProperty = null;
	private SerializedProperty m_boundaryPlaneColourProperty = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_navMesh = target as NavMesh;
		m_boundsProperty = serializedObject.FindProperty("m_bounds");
		m_dataContainerProperty = serializedObject.FindProperty("m_dataContainer");
		//m_cellDimensionProperty = serializedObject.FindProperty("m_cellDimension");
		m_editorNodePrototypeProperty = serializedObject.FindProperty("m_editorNodePrototype");

		m_nodeColourProperty = serializedObject.FindProperty("m_nodeColour");
		m_edgeColourProperty = serializedObject.FindProperty("m_edgeColour");
		m_boundaryPlaneColourProperty = serializedObject.FindProperty("m_boundaryPlaneColour");
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		
		if (m_navMesh != null)
		{
			if (m_boundsProperty != null)
			{
				EditorGUILayout.PropertyField(m_boundsProperty);
			}

			if (m_dataContainerProperty != null)
			{
				EditorGUILayout.PropertyField(m_dataContainerProperty);
			}

			// manual
			GUILayout.Space(12);
			if (GUILayout.Button("Edit"))
			{
				m_enableEdit = !m_enableEdit;
				if (m_enableEdit)
				{
					m_navMesh.Editor_GenerateManualEditNodes();
				}
				else
				{
					m_navMesh.Editor_RemoveManualEditNodes();
				}
			}
			EditorGUILayout.PropertyField(m_editorNodePrototypeProperty);

			// automatic
			//GUILayout.Space(12);
			//EditorGUILayout.LabelField("Automatic generation", EditorStyles.boldLabel);
			//GUILayout.BeginHorizontal();
			//m_cellDimensionProperty.floatValue = EditorGUILayout.FloatField("Cell Dimension", m_cellDimensionProperty.floatValue);
			//if (GUILayout.Button("Generate Grid"))
			//{
			//	m_navMesh.Editor_GenerateUniformGraph();
			//}
			//GUILayout.EndHorizontal();
			
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
			EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
			float cachedLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 64.0f;
			GUILayout.BeginHorizontal();
			DrawDebugColour(m_nodeColourProperty, "Node");
			DrawDebugColour(m_edgeColourProperty, "Edge");
			DrawDebugColour(m_boundaryPlaneColourProperty, "Boundary");
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