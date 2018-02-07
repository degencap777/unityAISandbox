using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PerceptionEventTypeFloatDictionary))]
public class PerceptionEventTypeFloatDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer<PerceptionEventType, float>
{

	protected override void DrawKey(SerializedProperty keys, int index, Rect keyRect)
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override void DrawValue(SerializedProperty values, int index, Rect valueRect)
	{
		;
	}


	// --------------------------------------------------------------------------------

	protected override void AddNew(SerializedProperty keys, SerializedProperty values)
	{
		keys.InsertArrayElementAtIndex(keys.arraySize);
		values.InsertArrayElementAtIndex(values.arraySize);
	}

}