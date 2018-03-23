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
			EditorGUILayout.TextField("Allegiance name", allegianceComponent.Editor_Settings.AllegianceName);
		}
	}

}