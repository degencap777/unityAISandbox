using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer
{
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
		Enum targetEnum = GetBaseProperty<Enum>(property);

		string propName = flagSettings.m_enumName;
		if (string.IsNullOrEmpty(propName))
			propName = property.name;

		EditorGUI.BeginProperty(position, label, property);
		Enum enumNew = EditorGUI.EnumFlagsField(position, propName, targetEnum);
		property.intValue = (int)Convert.ChangeType(enumNew, targetEnum.GetType());
		EditorGUI.EndProperty();
	}

	// --------------------------------------------------------------------------------

	static T GetBaseProperty<T>(SerializedProperty prop)
	{
		// separate the steps it takes to get to this property
		string[] separatedPaths = prop.propertyPath.Split('.');

		// go down to the root of this serialized property
		System.Object reflectionTarget = prop.serializedObject.targetObject as object;
		// walk down the path to get the target object
		foreach (var path in separatedPaths)
		{
			FieldInfo fieldInfo = reflectionTarget.GetType().GetField(path);
			reflectionTarget = fieldInfo.GetValue(reflectionTarget);
		}
		return (T)reflectionTarget;
	}

}