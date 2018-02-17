using UnityEditor;
using UnityEngine;

public abstract class SerializableDictionaryPropertyDrawer<K, V> : PropertyDrawer
{

	private static readonly float k_gap = 10.0f;

	// --------------------------------------------------------------------------------

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty keysProperty = property.FindPropertyRelative("m_keys");
		SerializedProperty valuesProperty = property.FindPropertyRelative("m_values");

		float h = EditorGUIUtility.singleLineHeight;
		float w = position.width;
		float paddedLineHeight = h + EditorGUIUtility.standardVerticalSpacing;

		for (int i = 0; i < keysProperty.arraySize; ++i)
		{
			float y = position.y + (k_gap * i) + paddedLineHeight * i * 3;
			
			Rect keyRect = new Rect(position.x, y, w, h);
			SerializedProperty keyProperty = keysProperty.GetArrayElementAtIndex(i);
			EditorGUI.PropertyField(keyRect, keyProperty);

			Rect valueRect = new Rect(position.x, y + paddedLineHeight, w, h);
			SerializedProperty valueProperty = valuesProperty.GetArrayElementAtIndex(i);
			EditorGUI.PropertyField(valueRect, valueProperty);

			Rect deleteButtonRect = new Rect(position.x, y + paddedLineHeight * 2, w, h);
			if (GUI.Button(deleteButtonRect, "Delete"))
			{
				keysProperty.DeleteArrayElementAtIndex(i);
				valuesProperty.DeleteArrayElementAtIndex(i);
			}
		}

		float addButtonRectY = position.y + keysProperty.arraySize * k_gap + keysProperty.arraySize * paddedLineHeight * 3;
		Rect addButtonRect = new Rect(position.x, addButtonRectY, w, h);
		if (GUI.Button(addButtonRect, "Add"))
		{
			++keysProperty.arraySize;
			SetDefaultPropertyValue(keysProperty.GetArrayElementAtIndex(keysProperty.arraySize - 1));
			
			++valuesProperty.arraySize;
			SetDefaultPropertyValue(valuesProperty.GetArrayElementAtIndex(valuesProperty.arraySize - 1));
			
			property.serializedObject.ApplyModifiedProperties();
		}

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		float paddedLineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		SerializedProperty keysProperty = property.FindPropertyRelative("m_keys");
		// rows
		float height = keysProperty.arraySize * 3 * paddedLineHeight + keysProperty.arraySize * k_gap;
		// add button
		height += paddedLineHeight;
		return height;
	}

	// --------------------------------------------------------------------------------

	private void SetDefaultPropertyValue(SerializedProperty property)
	{
		switch (property.propertyType)
		{
			case SerializedPropertyType.Generic: // array
				property.objectReferenceValue = null;
				break;
			case SerializedPropertyType.Integer:
				property.intValue = default(int);
				break;
			case SerializedPropertyType.Boolean:
				property.boolValue = default(bool);
				break;
			case SerializedPropertyType.Float:
				property.floatValue = default(float);
				break;
			case SerializedPropertyType.String:
				property.stringValue = default(string);
				break;
			case SerializedPropertyType.Color:
				property.colorValue = default(Color);
				break;
			case SerializedPropertyType.ObjectReference:
				property.objectReferenceValue = null;
				break;
			case SerializedPropertyType.LayerMask:
				property.intValue = 0;
				break;
			case SerializedPropertyType.Enum:
				property.intValue = 0;
				break;
			case SerializedPropertyType.Vector2:
				property.vector2Value = default(Vector2);
				break;
			case SerializedPropertyType.Vector3:
				property.vector3Value = default(Vector3);
				break;
			case SerializedPropertyType.Vector4:
				property.vector4Value = default(Vector4);
				break;
			case SerializedPropertyType.Rect:
				property.rectValue = default(Rect);
				break;
			case SerializedPropertyType.ArraySize:
				property.arraySize = 0;
				break;
			case SerializedPropertyType.Character:
				property.stringValue = default(string);
				break;
			case SerializedPropertyType.AnimationCurve:
				property.animationCurveValue = default(AnimationCurve);
				break;
			case SerializedPropertyType.Bounds:
				property.boundsValue = default(Bounds);
				break;
			case SerializedPropertyType.Gradient: // Gradient used for animating colours
				// ??
				break;
			case SerializedPropertyType.Quaternion:
				property.quaternionValue = default(Quaternion);
				break;
			case SerializedPropertyType.ExposedReference: // reference to another Object in the Scene
				property.objectReferenceValue = null;
				break;
			case SerializedPropertyType.FixedBufferSize:
				// ??
				break;
			case SerializedPropertyType.Vector2Int:
				property.vector2IntValue = default(Vector2Int);
				break;
			case SerializedPropertyType.Vector3Int:
				property.vector3IntValue = default(Vector3Int);
				break;
			case SerializedPropertyType.RectInt:
				property.rectIntValue = default(RectInt);
				break;
			case SerializedPropertyType.BoundsInt:
				property.boundsIntValue = default(BoundsInt);
				break;
		}
	}

}