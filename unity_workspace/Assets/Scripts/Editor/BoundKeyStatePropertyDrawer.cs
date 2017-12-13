using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BoundKeyState))]
public class BoundKeyStatePropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label.text = "Key state";

		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		var x = position.x;
		var y = position.y;
		var h = position.height / 2;
		var sectionWidth = position.width / 2;
		
		SerializedProperty keycodeProperty = property.FindPropertyRelative("m_requiredKeyCode");
		SerializedProperty keyStateProperty = property.FindPropertyRelative("m_requiredKeyState");
		SerializedProperty durationProperty = property.FindPropertyRelative("m_requiredDuration");

		int held = (int)KeyState.Held;
		bool includesHeld = (keyStateProperty.intValue & held) == held;
		if (includesHeld)
		{
			h = position.height / 3;
		}

		EditorGUI.LabelField(new Rect(x, y, sectionWidth - 2, h), "Key code");
		keycodeProperty.intValue = (int)(KeyCode)EditorGUI.EnumPopup(new Rect(x + sectionWidth, y, sectionWidth - 2, h - 2), (KeyCode)keycodeProperty.intValue);

		EditorGUI.LabelField(new Rect(x, y + h, sectionWidth - 2, h), "Key state(s)");
		keyStateProperty.intValue = (int)(KeyState)EditorGUI.EnumMaskField(new Rect(x + sectionWidth, y + h, sectionWidth - 2, h - 2), GUIContent.none, (KeyState)keyStateProperty.intValue);

		if (includesHeld)
		{
			EditorGUI.LabelField(new Rect(x, y + h * 2, sectionWidth - 2, h), "Required duration");
			durationProperty.floatValue = EditorGUI.FloatField(new Rect(x + sectionWidth, y + h * 2, sectionWidth - 2, h - 2), GUIContent.none, durationProperty.floatValue);
		}

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		SerializedProperty keyStateProperty = property.FindPropertyRelative("m_requiredKeyState");
		int held = (int)KeyState.Held;

		if ((keyStateProperty.intValue & held) == held)
		{
			return 54.0f;
		}
		return 36.0f;
	}

}