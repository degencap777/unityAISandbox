using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumMaskFieldAttribute))]
public class EnumMaskFieldPropertyDrawer : PropertyDrawer
{

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EnumMaskFieldAttribute attr = attribute as EnumMaskFieldAttribute;
		string fieldName = property.name;
		if (attr != null && !string.IsNullOrEmpty(attr.fieldName))
		{
			fieldName = attr.fieldName;
		}

		// get the enum type via reflection (thanks to PropertyDrawer.fieldInfo) and create an enum value at runtime
		// using the type and the property.intValue
		System.Enum enumValue = System.Enum.ToObject(fieldInfo.FieldType, property.intValue) as System.Enum;
		if (enumValue != null)
		{
			EditorGUI.BeginProperty(position, label, property);
			System.Enum enumOutput = EditorGUI.EnumMaskField(position, fieldName, enumValue);
			property.intValue = (int)System.Convert.ChangeType(enumOutput, enumValue.GetType());

			// EnumMaskField returns -1 if you select all mask options, this is a bit weird...
			// make it return the correct value (combining all of the enum values) instead
			if (property.intValue == -1)
			{
				int[] values = System.Enum.GetValues(enumValue.GetType()) as int[];
				property.intValue = 0;
				for (int i = 0; i < values.Length; ++i)
				{
					property.intValue += values[i];
				}
			}

			EditorGUI.EndProperty();
		}
	}

}
