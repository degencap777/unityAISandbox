using UnityEditor;

[CustomEditor(typeof(DamageableComponent))]
[CanEditMultipleObjects]
public class DamageableComponentEditor : ComponentEditor
{

	protected override void DrawSettings()
	{
		DamageableComponent damageComponent = m_component as DamageableComponent;
		if (damageComponent != null && damageComponent.Editor_Settings != null)
		{
			EditorGUILayout.FloatField("Max health", damageComponent.Editor_Settings.MaxHealth);
			EditorGUILayout.FloatField("Damage scalar", damageComponent.Editor_Settings.DamageScalar);
		}
	}

}