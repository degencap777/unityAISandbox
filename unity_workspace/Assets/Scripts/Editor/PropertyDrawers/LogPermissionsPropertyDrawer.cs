using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LogPermissions))]
public class LogPermissionsPropertyDrawer : PropertyDrawer
{

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		
		var x = position.x;
		var y = position.y;
		var h = position.height;
		var w = position.width;

		SerializedProperty tagProperty = property.FindPropertyRelative("m_tag");
		SerializedProperty infoProperty = property.FindPropertyRelative("m_info");
		SerializedProperty warningProperty = property.FindPropertyRelative("m_warning");
		SerializedProperty errorProperty = property.FindPropertyRelative("m_error");

		int cachedIndentLevel = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		float labelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 16.0f;

		tagProperty.stringValue = EditorGUI.TextField(new Rect(x, y, w * 0.5f, h), tagProperty.stringValue);
		infoProperty.boolValue = EditorGUI.Toggle(new Rect(x + w * 0.55f, y, w * 0.15f, h), "I", infoProperty.boolValue);
		warningProperty.boolValue = EditorGUI.Toggle(new Rect(x + w * 0.7f, y, w * 0.15f, h), "W", warningProperty.boolValue);
		errorProperty.boolValue = EditorGUI.Toggle(new Rect(x + w * 0.85f, y, w * 0.15f, h), "E", errorProperty.boolValue);

		EditorGUIUtility.labelWidth = labelWidth;
		EditorGUI.indentLevel = cachedIndentLevel;
		EditorGUI.EndProperty();
	}

}