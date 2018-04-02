using AISandbox.Utility;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AreaBounds))]
public class AreaBoundsPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		float cachedLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 64.0f;

		SerializedProperty minProperty = property.FindPropertyRelative("m_minBounds");
		SerializedProperty maxProperty = property.FindPropertyRelative("m_maxBounds");
		
		var x = position.x;
		var y = position.y;
		var w = position.width;
		var h = position.height / 2;

		if (minProperty != null)
		{
			minProperty.vector3Value = EditorGUI.Vector3Field(new Rect(x, y, w, h), "Min", minProperty.vector3Value);
		}
		if (maxProperty != null)
		{
			maxProperty.vector3Value = EditorGUI.Vector3Field(new Rect(x, y + h, w, h), "Max", maxProperty.vector3Value);
		}

		EditorGUIUtility.labelWidth = cachedLabelWidth;
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;
	}

}