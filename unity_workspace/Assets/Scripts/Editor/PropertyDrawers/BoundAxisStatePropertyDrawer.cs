using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BoundAxisState))]
public class BoundAxisStatePropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label.text = "Axis state";

		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		var x = position.x;
		var y = position.y;
		var h = position.height / 2;
		var sectionWidth = position.width / 2;
		
		SerializedProperty axisProperty = property.FindPropertyRelative("m_requiredAxis");
		SerializedProperty deadzoneProperty = property.FindPropertyRelative("m_deadZone");

		EditorGUI.LabelField(new Rect(x, y, sectionWidth - 2, h), "Axis name");
		axisProperty.stringValue = EditorGUI.TextField(new Rect(x + sectionWidth, y, sectionWidth - 2, h - 2), GUIContent.none, axisProperty.stringValue);

		EditorGUI.LabelField(new Rect(x, y + h, sectionWidth - 2, h), "Deadzone");
		deadzoneProperty.floatValue = EditorGUI.FloatField(new Rect(x + sectionWidth, y + h, sectionWidth - 2, h - 2), GUIContent.none, deadzoneProperty.floatValue);

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return 36.0f;
	}

}