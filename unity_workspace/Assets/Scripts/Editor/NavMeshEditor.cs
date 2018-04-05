using AISandbox.Navigation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavMesh), true)]
public class NavMeshEditor : Editor
{

	private NavMesh m_navMesh = null;
	private SerializedProperty m_boundsProperty = null;
	private SerializedProperty m_dataContainerProperty = null;
	private SerializedProperty m_cellDimensionProperty = null;
	private bool m_enableManualPlacement = false;

	private SerializedProperty m_nodeColourProperty = null;
	private SerializedProperty m_edgeColourProperty = null;
	private SerializedProperty m_boundaryPlaneColourProperty = null;

	// --------------------------------------------------------------------------------

	public void OnEnable()
	{
		m_navMesh = target as NavMesh;
		m_boundsProperty = serializedObject.FindProperty("m_bounds");
		m_dataContainerProperty = serializedObject.FindProperty("m_dataContainer");
		m_cellDimensionProperty = serializedObject.FindProperty("m_cellDimension");

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

			// automatic/manual generation
			GUILayout.Space(6);
			if (GUILayout.Button("Toggle Automatic/Manual Node Generation"))
			{
				m_enableManualPlacement = !m_enableManualPlacement;
			}
			
			GUILayout.Space(6);
			if (m_enableManualPlacement)
			{
				// manual
				EditorGUILayout.LabelField("Manual generation", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("pending");
			}
			else
			{
				// automatic
				EditorGUILayout.LabelField("Automatic generation", EditorStyles.boldLabel);
				GUILayout.BeginHorizontal();
				m_cellDimensionProperty.floatValue = EditorGUILayout.FloatField("Cell Dimension", m_cellDimensionProperty.floatValue);
				if (GUILayout.Button("Generate Grid"))
				{
					m_navMesh.Editor_GenerateUniformGraph();
				}
				GUILayout.EndHorizontal();
			}

			// persistence
			GUILayout.Space(6);
			EditorGUILayout.LabelField("Persistence", EditorStyles.boldLabel);
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