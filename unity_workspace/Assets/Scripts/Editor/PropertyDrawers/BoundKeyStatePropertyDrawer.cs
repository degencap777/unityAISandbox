using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BoundKeyState))]
[CanEditMultipleObjects]
public class BoundKeyStatePropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label.text = "Key state";

		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty keycodeProperty = property.FindPropertyRelative("m_requiredKeyCode");
		SerializedProperty keyStateProperty = property.FindPropertyRelative("m_requiredKeyState");
		SerializedProperty durationProperty = property.FindPropertyRelative("m_requiredDuration");

		bool isHeld = (KeyState)keyStateProperty.intValue == KeyState.Held;

		var x = position.x;
		var y = position.y;
		var h = position.height / (isHeld ? 3 : 2);
		var sectionWidth = position.width / 2;
		
		EditorGUI.LabelField(new Rect(x, y, sectionWidth - 2, h), "Key code");
		keycodeProperty.intValue = (int)(KeyCode)EditorGUI.EnumPopup(new Rect(x + sectionWidth, y, sectionWidth - 2, h - 2), (KeyCode)keycodeProperty.intValue);

		EditorGUI.LabelField(new Rect(x, y + h, sectionWidth - 2, h), "Key state");
		keyStateProperty.intValue = (int)(KeyState)EditorGUI.EnumPopup(new Rect(x + sectionWidth, y + h, sectionWidth - 2, h - 2), GUIContent.none, (KeyState)keyStateProperty.intValue);

		if (isHeld)
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
		bool isHeld = (KeyState)keyStateProperty.intValue == KeyState.Held;
		return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * (isHeld ? 3 : 2);
	}

}