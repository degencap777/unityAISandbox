using UnityEditor;

[CustomEditor(typeof(HealthComponent))]
[CanEditMultipleObjects]
public class HealthComponentEditor : ComponentEditor
{

	protected override void DrawConfig()
	{
		HealthComponent healthComponent = m_component as HealthComponent;
		if (healthComponent != null && healthComponent.Editor_Config != null)
		{
			EditorGUILayout.FloatField("Max health", healthComponent.Editor_Config.MaxHealth);
			EditorGUILayout.FloatField("Damage scalar", healthComponent.Editor_Config.DamageScalar);
		}
	}

}