using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringIntDictionary))]
public class StringIntDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<string, int>
{

	protected override void DrawKey(SerializedProperty keys, int index, Rect keyRect)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(index);
		keyProperty.stringValue = EditorGUI.TextField(keyRect, keyProperty.stringValue);
	}

	// --------------------------------------------------------------------------------

	protected override void DrawValue(SerializedProperty values, int index, Rect valueRect)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(index);
		valueProperty.intValue = EditorGUI.IntField(valueRect, valueProperty.intValue);
	}

	// --------------------------------------------------------------------------------

	protected override void AddKey(SerializedProperty keys)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(keys.arraySize - 1);
		keyProperty.stringValue = "new key";
	}

	// --------------------------------------------------------------------------------

	protected override void AddValue(SerializedProperty values)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(values.arraySize - 1);
		valueProperty.intValue = -1;
	}

}

// ------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------

[CustomPropertyDrawer(typeof(PerceptionEventTypeFloatDictionary))]
public class PerceptionEventTypeFloatDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<float, PerceptionEventType>
{

	protected override void DrawKey(SerializedProperty keys, int index, Rect keyRect)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(index);
		PerceptionEventType key = (PerceptionEventType)keyProperty.intValue;
		keyProperty.intValue = (int)(PerceptionEventType)EditorGUI.EnumPopup(keyRect, key);
	}

	// --------------------------------------------------------------------------------

	protected override void DrawValue(SerializedProperty values, int index, Rect valueRect)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(index);
		float value = valueProperty.floatValue;
		valueProperty.floatValue = EditorGUI.FloatField(valueRect, value);
	}

	// --------------------------------------------------------------------------------

	protected override void AddKey(SerializedProperty keys)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(keys.arraySize - 1);
		keyProperty.intValue = (int)PerceptionEventType.None;
	}

	// --------------------------------------------------------------------------------

	protected override void AddValue(SerializedProperty values)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(values.arraySize - 1);
		valueProperty.floatValue = -1.0f;
	}
}