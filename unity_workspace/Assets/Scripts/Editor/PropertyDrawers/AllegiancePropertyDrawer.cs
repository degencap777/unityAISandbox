using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Allegiance))]
public class AllegiancePropertyDrawer : PropertyDrawer
{

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var x = position.x;
		var y = position.y;
		var h = position.height;
		var w = position.width;

		SerializedProperty nameProperty = property.FindPropertyRelative("m_name");
		SerializedProperty colourProperty = property.FindPropertyRelative("m_colour");
		
		int cachedIndentLevel = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		float cachedLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 32.0f;

		float nameWidth = w * 0.6f;
		Rect nameRect = new Rect(x, y, nameWidth, h);
		nameProperty.stringValue = EditorGUI.TextField(nameRect, nameProperty.stringValue);

		float colWidth = w * 0.3f;
		float colmargin = w * 0.1f;
		Rect colRect = new Rect(x + nameWidth + colmargin, y, colWidth, h);
		colourProperty.colorValue = EditorGUI.ColorField(colRect, colourProperty.colorValue);
		
		EditorGUIUtility.labelWidth = cachedLabelWidth;
		EditorGUI.indentLevel = cachedIndentLevel;
		EditorGUI.EndProperty();
	}
	
}