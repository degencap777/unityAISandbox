using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LogPermissions))]
public class LogPermissionsPropertyDrawer : PropertyDrawer
{

	private static readonly int k_lineCount = 4;

	// --------------------------------------------------------------------------------

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		
		var x = position.x;
		var y = position.y;
		var h = position.height / k_lineCount;
		var w = position.width;

		SerializedProperty tagProperty = property.FindPropertyRelative("m_tag");
		SerializedProperty infoProperty = property.FindPropertyRelative("m_info");
		SerializedProperty warningProperty = property.FindPropertyRelative("m_warning");
		SerializedProperty errorProperty = property.FindPropertyRelative("m_error");

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		
		tagProperty.stringValue = EditorGUI.TextField(new Rect(x, y, w, h), tagProperty.stringValue);
		infoProperty.boolValue = EditorGUI.Toggle(new Rect(x, y + h, w, h), "Info", infoProperty.boolValue);
		warningProperty.boolValue = EditorGUI.Toggle(new Rect(x, y + (h * 2), w, h), "Warning", warningProperty.boolValue);
		errorProperty.boolValue = EditorGUI.Toggle(new Rect(x, y + (h * 3), w, h), "Error", errorProperty.boolValue);

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * k_lineCount;
	}

}