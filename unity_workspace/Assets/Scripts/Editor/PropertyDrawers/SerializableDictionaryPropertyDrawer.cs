using UnityEditor;
using UnityEngine;

public abstract class SerializableDictionaryPropertyDrawer<K, V> : PropertyDrawer
{

	protected abstract void DrawKey(SerializedProperty keys, int index, Rect keyRect);
	protected abstract void AddKey(SerializedProperty keys);
	protected abstract void DrawValue(SerializedProperty values, int index, Rect valueRect);
	protected abstract void AddValue(SerializedProperty values);

	// --------------------------------------------------------------------------------

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty keysProperty = property.FindPropertyRelative("m_keys");
		SerializedProperty valuesProperty = property.FindPropertyRelative("m_values");

		float lineHeight = EditorGUIUtility.singleLineHeight;
		float paddedLineHeight = lineHeight + EditorGUIUtility.standardVerticalSpacing;

		for (int i = 0; i < keysProperty.arraySize; ++i)
		{
			Rect keyRect = new Rect(position.x, position.y + paddedLineHeight * i, position.width * 0.35f, lineHeight);
			DrawKey(keysProperty, i, keyRect);

			Rect valueRect = new Rect(position.x + position.width * 0.375f, position.y + paddedLineHeight * i, position.width * 0.35f, lineHeight);
			DrawValue(valuesProperty, i, valueRect);

			Rect deleteButtonRect = new Rect(position.x + position.width * 0.75f, position.y + paddedLineHeight * i, position.width * 0.25f, lineHeight);
			if (GUI.Button(deleteButtonRect, "Delete"))
			{
				keysProperty.DeleteArrayElementAtIndex(i);
				valuesProperty.DeleteArrayElementAtIndex(i);
			}
		}

		Rect addButtonRect = new Rect(position.x + position.width * 0.25f, position.y + keysProperty.arraySize * paddedLineHeight, position.width * 0.5f, lineHeight);
		if (GUI.Button(addButtonRect, "Add"))
		{
			++keysProperty.arraySize;
			AddKey(keysProperty);
			++valuesProperty.arraySize;
			AddValue(valuesProperty);
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