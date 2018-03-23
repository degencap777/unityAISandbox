using UnityEditor;

[CustomEditor(typeof(CollisionComponent))]
[CanEditMultipleObjects]
public class CollisionComponentEditor : ComponentEditor
{

	protected override void DrawSettings()
	{
		CollisionComponent collisionComponent = m_component as CollisionComponent;
		if (collisionComponent != null && collisionComponent.Editor_Settings != null)
		{
			;
		}
	}

}