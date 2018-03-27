using UnityEditor;

[CustomEditor(typeof(DamageableComponent))]
[CanEditMultipleObjects]
public class DamageableComponentEditor : ComponentEditor
{

	protected override void DrawConfig()
	{
		DamageableComponent damageComponent = m_component as DamageableComponent;
		if (damageComponent != null && damageComponent.Editor_Config != null)
		{
			EditorGUILayout.FloatField("Max health", damageComponent.Editor_Config.MaxHealth);
			EditorGUILayout.FloatField("Damage scalar", damageComponent.Editor_Config.DamageScalar);
		}
	}

}