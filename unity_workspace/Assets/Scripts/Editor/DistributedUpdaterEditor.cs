using UnityEditor;
using UnityEngine;

public abstract class DistributedUpdaterEditor : Editor
{

	protected abstract int GetBucketsCount(Object target);
	protected abstract float GetBucketUpdateTime(Object target, int bucketIndex);
	protected abstract int GetBucketContentsCount(Object target, int bucketIndex);

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();

		GUILayout.Label("Buckets", EditorStyles.boldLabel);

		// cache and increase indent
		int cachedIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel += 2;

		EditorGUI.BeginDisabledGroup(true);
		for (int i = 0; i < GetBucketsCount(target); ++i)
		{
			EditorGUILayout.LabelField(string.Format("buckets [{0}]", i));
			
			EditorGUI.indentLevel += 2;

			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Time offset");
			EditorGUILayout.TextField(string.Format("{0:F3}", GetBucketUpdateTime(target, i)));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Content count");
			EditorGUILayout.TextField(string.Format("{0}", GetBucketContentsCount(target, i)));
			GUILayout.EndHorizontal();

			EditorGUI.indentLevel -= 2;
		}
		EditorGUI.EndDisabledGroup();

		// reset indent
		EditorGUI.indentLevel = cachedIndent;

		serializedObject.ApplyModifiedProperties();
	}
	
}