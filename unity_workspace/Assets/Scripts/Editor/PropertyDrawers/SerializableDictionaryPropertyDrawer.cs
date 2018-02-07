using UnityEditor;
using UnityEngine;

public abstract class SerializableDictionaryPropertyDrawer<K, V> : PropertyDrawer
{

	protected abstract void DrawKey(SerializedProperty keys, int index, Rect keyRect);
	protected abstract void DrawValue(SerializedProperty values, int index, Rect valueRect);
	protected abstract void AddNew(SerializedProperty keys, SerializedProperty values);

	// --------------------------------------------------------------------------------

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty keysProperty = property.FindPropertyRelative("m_keys");
		SerializedProperty valuesProperty = property.FindPropertyRelative("m_values");

		float paddedLineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		for (int i = 0; i < keysProperty.arraySize; ++i)
		{
			Rect keyRect = new Rect(0.0f, 0.0f, position.width * 0.4f, paddedLineHeight);
			DrawKey(keysProperty, i, keyRect);

			Rect valueRect = new Rect(position.width * 0.4f, 0.0f, position.width * 0.4f, paddedLineHeight);
			DrawValue(valuesProperty, i, valueRect);

			Rect deleteButtonRect = new Rect(position.width * 0.8f, 0.0f, position.width * 0.1f, paddedLineHeight);
			if (GUI.Button(deleteButtonRect, "Delete"))
			{
				keysProperty.DeleteArrayElementAtIndex(i);
				valuesProperty.DeleteArrayElementAtIndex(i);
			}
		}

		Rect addButtonRect = new Rect(0.0f, keysProperty.arraySize * paddedLineHeight, position.width * 0.33f, paddedLineHeight);
		if (GUI.Button(addButtonRect, "Add"))
		{
			AddNew(keysProperty, valuesProperty);
		}

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		float paddedLineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		SerializedProperty keysProperty = property.FindPropertyRelative("m_keys");
		// keys & values
		float height = keysProperty.arraySize * paddedLineHeight;
		// add button
		height += paddedLineHeight;
		return height;
	}

}