using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(SerializableDictionary<K, V>))]
public abstract class SerializableDictionaryPropertyDrawer<K, V> : PropertyDrawer
{

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty keysProperty = property.FindPropertyRelative("m_keys");
		SerializedProperty valuesProperty = property.FindPropertyRelative("m_values");

		for (int i = 0; i < keysProperty.arraySize; ++i)
		{
			// #SteveD	>>> [ Key ] [ Value ]   [ Remove button ]
			//			>>> abstract methods implemented in derived class
		}

		// #SteveD	>>> [ Add button ]
		//			>>> abstract method implemented in derived class

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();

	}

}