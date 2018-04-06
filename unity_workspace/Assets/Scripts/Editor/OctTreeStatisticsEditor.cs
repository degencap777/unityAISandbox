using AISandbox.Container;
using AISandbox.Navigation;
using UnityEditor;

[CustomEditor(typeof(OctTreeStatistics))]
public class OctTreeStatisticsEditor : Editor
{

	private OctTreeStatistics m_stats = null;

	// --------------------------------------------------------------------------------

	protected virtual void OnEnable()
	{
		m_stats = target as OctTreeStatistics;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		float cachedLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 64.0f;

		if (m_stats != null)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Total Nodes", EditorStyles.boldLabel);
			EditorGUILayout.LabelField(string.Format("Current [{0}]", m_stats.m_currentNodes));
			EditorGUILayout.LabelField(string.Format("Peak [{0}]", m_stats.m_peakNodes));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Leaf Nodes", EditorStyles.boldLabel);
			EditorGUILayout.LabelField(string.Format("Current [{0}]", m_stats.m_currentLeafNodes));
			EditorGUILayout.LabelField(string.Format("Peak [{0}]", m_stats.m_peakLeafNodes));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Migrants", EditorStyles.boldLabel);
			EditorGUILayout.LabelField(string.Format("Current [{0}]", m_stats.m_currentMigrants));
			EditorGUILayout.LabelField(string.Format("Peak [{0}]", m_stats.m_peakMigrants));
			EditorGUILayout.EndHorizontal();
		}

		EditorGUIUtility.labelWidth = cachedLabelWidth;

		serializedObject.ApplyModifiedProperties();
	}

}