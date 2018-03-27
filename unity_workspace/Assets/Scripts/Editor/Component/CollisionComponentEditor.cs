using UnityEditor;

[CustomEditor(typeof(CollisionComponent))]
[CanEditMultipleObjects]
public class CollisionComponentEditor : ComponentEditor
{

	protected override void DrawConfig()
	{
		CollisionComponent collisionComponent = m_component as CollisionComponent;
		if (collisionComponent != null && collisionComponent.Editor_Config != null)
		{
			;
		}
	}

}