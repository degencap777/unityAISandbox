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

		SerializedProperty idProperty = property.FindPropertyRelative("m_id");
		SerializedProperty nameProperty = property.FindPropertyRelative("m_name");
		SerializedProperty colourProperty = property.FindPropertyRelative("m_colour");
		
		int cachedIndentLevel = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		float cachedLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 32.0f;

		float uidWidth = w * 0.2f;
		Rect uidRect = new Rect(x, y, uidWidth, h);
		EditorGUI.LabelField(uidRect, string.Format("[{0}]", idProperty.intValue.ToString()));

		float nameWidth = w * 0.4f;
		Rect nameRect = new Rect(x + uidWidth, y, nameWidth, h);
		nameProperty.stringValue = EditorGUI.TextField(nameRect, nameProperty.stringValue);

		float colWidth = w * 0.3f;
		float colmargin = w * 0.1f;
		Rect colRect = new Rect(x + uidWidth + nameWidth + colmargin, y, colWidth, h);
		colourProperty.colorValue = EditorGUI.ColorField(colRect, colourProperty.colorValue);
		
		EditorGUIUtility.labelWidth = cachedLabelWidth;
		EditorGUI.indentLevel = cachedIndentLevel;
		EditorGUI.EndProperty();
	}
	
}