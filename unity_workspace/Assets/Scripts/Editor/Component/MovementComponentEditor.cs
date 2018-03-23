using UnityEditor;

[CustomEditor(typeof(MovementComponent))]
[CanEditMultipleObjects]
public class MovementComponentEditor : ComponentEditor
{

	protected override void DrawSettings()
	{
		MovementComponent movementComponent = m_component as MovementComponent;
		if (movementComponent != null && movementComponent.Editor_Settings != null)
		{
			EditorGUILayout.FloatField("Move forward speed", movementComponent.Editor_Settings.MoveForwardSpeed);
			EditorGUILayout.FloatField("Move backward speed", movementComponent.Editor_Settings.MoveBackwardSpeed);
			EditorGUILayout.FloatField("Move sideways speed", movementComponent.Editor_Settings.MoveSidewaysSpeed);
			EditorGUILayout.FloatField("Movement acceleration factor", movementComponent.Editor_Settings.MovementAccelerationFactor);
			EditorGUILayout.FloatField("Movement deceleration factor", movementComponent.Editor_Settings.MovementDecelerationFactor);
			EditorGUILayout.FloatField("Rotation speed", movementComponent.Editor_Settings.RotationSpeed);
			EditorGUILayout.FloatField("Rotation acceleration factor", movementComponent.Editor_Settings.RotationAccelerationFactor);
			EditorGUILayout.FloatField("Rotation deceleration factor", movementComponent.Editor_Settings.RotationDecelerationFactor);
		}
	}

}