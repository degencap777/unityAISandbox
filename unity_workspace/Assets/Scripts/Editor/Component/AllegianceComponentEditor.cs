using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllegianceComponent))]
[CanEditMultipleObjects]
public class AllegianceComponentEditor : ComponentEditor
{

	protected override void DrawConfig()
	{
		AllegianceComponent allegianceComponent = m_component as AllegianceComponent;
		if (allegianceComponent != null && allegianceComponent.Editor_Config != null)
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.TextField(allegianceComponent.Editor_Config.Name);
			EditorGUILayout.ColorField(allegianceComponent.Editor_Config.Colour);
			GUILayout.EndHorizontal();
		}
	}

}