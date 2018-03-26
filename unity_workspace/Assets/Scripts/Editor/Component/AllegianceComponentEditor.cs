using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllegianceComponent))]
[CanEditMultipleObjects]
public class AllegianceComponentEditor : ComponentEditor
{

	protected override void DrawSettings()
	{
		AllegianceComponent allegianceComponent = m_component as AllegianceComponent;
		if (allegianceComponent != null && allegianceComponent.Editor_Settings != null)
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.TextField(allegianceComponent.Editor_Settings.Allegiance.Name);
			EditorGUILayout.ColorField(allegianceComponent.Editor_Settings.Allegiance.Colour);
			GUILayout.EndHorizontal();
		}
	}

}