using AISandbox.Variable;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
[CanEditMultipleObjects]
public class FloatReferencePropertyDrawer : PropertyDrawer
{

	private static readonly float k_dropdownWidth = 64.0f;

	private static readonly float k_gapWidth = 8.0f;

	private static readonly float k_minValueWidth = 128.0f;
	private static readonly float k_maxValueWidth = 256.0f;

	// --------------------------------------------------------------------------------

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		var x = position.x;
		var y = position.y;
		var h = position.height;
		var w = position.width;

		SerializedProperty useValueProperty = property.FindPropertyRelative("m_useValue");
		Rect useValuePopupRect = new Rect(x, y, k_dropdownWidth, h);

		VariableReferenceType refType = useValueProperty.boolValue ? VariableReferenceType.Value : VariableReferenceType.Variable;
		refType = (VariableReferenceType)EditorGUI.EnumPopup(useValuePopupRect, GUIContent.none, (VariableReferenceType)useValueProperty.intValue);
		useValueProperty.boolValue = refType == VariableReferenceType.Value;

		float takenWidth = k_dropdownWidth + k_gapWidth;
		Rect valueRect = new Rect(x + takenWidth, y, Mathf.Clamp(w - takenWidth, k_minValueWidth, k_maxValueWidth), h);
		if (useValueProperty.boolValue)
		{
			SerializedProperty valueProperty = property.FindPropertyRelative("m_value");
			valueProperty.floatValue = EditorGUI.FloatField(valueRect, valueProperty.floatValue);
		}
		else
		{
			//float labelWidth = EditorGUIUtility.labelWidth;
			//EditorGUIUtility.labelWidth = 0.0f;

			SerializedProperty variableProperty = property.FindPropertyRelative("m_variable");
			EditorGUI.PropertyField(valueRect, variableProperty, GUIContent.none);

			//EditorGUIUtility.labelWidth = labelWidth;
		}

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

}