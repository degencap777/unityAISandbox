using UnityEditor;

[CustomEditor(typeof(MovementComponent))]
[CanEditMultipleObjects]
public class MovementComponentEditor : ComponentEditor
{

	protected override void DrawConfig()
	{
		MovementComponent movementComponent = m_component as MovementComponent;
		if (movementComponent != null && movementComponent.Editor_Config != null)
		{
			EditorGUILayout.FloatField("Move forward speed", movementComponent.Editor_Config.MoveForwardSpeed);
			EditorGUILayout.FloatField("Move backward speed", movementComponent.Editor_Config.MoveBackwardSpeed);
			EditorGUILayout.FloatField("Move sideways speed", movementComponent.Editor_Config.MoveSidewaysSpeed);
			EditorGUILayout.FloatField("Movement acceleration factor", movementComponent.Editor_Config.MovementAccelerationFactor);
			EditorGUILayout.FloatField("Movement deceleration factor", movementComponent.Editor_Config.MovementDecelerationFactor);
			EditorGUILayout.FloatField("Rotation speed", movementComponent.Editor_Config.RotationSpeed);
			EditorGUILayout.FloatField("Rotation acceleration factor", movementComponent.Editor_Config.RotationAccelerationFactor);
			EditorGUILayout.FloatField("Rotation deceleration factor", movementComponent.Editor_Config.RotationDecelerationFactor);
		}
	}

}