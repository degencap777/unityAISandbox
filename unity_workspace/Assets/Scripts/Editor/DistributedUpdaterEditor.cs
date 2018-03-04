using UnityEditor;
using UnityEngine;

public abstract class DistributedUpdaterEditor : Editor
{

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();
		
		// #SteveD	>>> fix. bucketsProperty is null

		SerializedProperty bucketsProperty = serializedObject.FindProperty("m_updatableBuckets");

		GUILayout.Label("Buckets", EditorStyles.boldLabel);

		// cache and increase indent
		int cachedIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel += 2;

		EditorGUI.BeginDisabledGroup(true);
		/*for (int i = 0; i < bucketsProperty.arraySize; ++i)
		{
			var bucket = bucketsProperty.GetArrayElementAtIndex(i);
			var bucketItems = bucket.FindPropertyRelative("m_updatables");
			var updateTime = bucket.FindPropertyRelative("m_updateTime");

			EditorGUILayout.LabelField(string.Format("buckets [{0}]", i));
			
			EditorGUI.indentLevel += 2;

			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Time offset");
			EditorGUILayout.TextField(string.Format("{0:F3}", updateTime));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Content count");
			EditorGUILayout.TextField(string.Format("{0}", bucketItems.arraySize));
			GUILayout.EndHorizontal();

			EditorGUI.indentLevel -= 2;
		}*/
		EditorGUI.EndDisabledGroup();

		// reset indent
		EditorGUI.indentLevel = cachedIndent;

		serializedObject.ApplyModifiedProperties();
	}
	
}