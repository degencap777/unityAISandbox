using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(String_Int_Dictionary))]
public class String_Int_DictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<string, int>
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

	protected override void SetDefaultKey(SerializedProperty keys, int index)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(index);
		keyProperty.stringValue = "new key";
	}

	// --------------------------------------------------------------------------------

	protected override void SetDefaultValue(SerializedProperty values, int index)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(index);
		valueProperty.intValue = -1;
	}

}

// ------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------

[CustomPropertyDrawer(typeof(PerceptionEventType_Float_Dictionary))]
public class PerceptionEventType_Float_DictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<float, PerceptionEventType>
{

	protected override void DrawKey(SerializedProperty keys, int index, Rect keyRect)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(index);
		keyProperty.intValue = (int)(PerceptionEventType)EditorGUI.EnumPopup(keyRect, (PerceptionEventType)keyProperty.intValue);
	}

	// --------------------------------------------------------------------------------

	protected override void DrawValue(SerializedProperty values, int index, Rect valueRect)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(index);
		valueProperty.floatValue = EditorGUI.FloatField(valueRect, valueProperty.floatValue);
	}

	// --------------------------------------------------------------------------------

	protected override void SetDefaultKey(SerializedProperty keys, int index)
	{
		SerializedProperty keyProperty = keys.GetArrayElementAtIndex(index);
		keyProperty.intValue = (int)PerceptionEventType.None;
	}

	// --------------------------------------------------------------------------------

	protected override void SetDefaultValue(SerializedProperty values, int index)
	{
		SerializedProperty valueProperty = values.GetArrayElementAtIndex(index);
		valueProperty.floatValue = -1.0f;
	}
}