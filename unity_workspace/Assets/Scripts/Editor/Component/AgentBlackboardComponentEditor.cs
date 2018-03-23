using UnityEditor;

[CustomEditor(typeof(AgentBlackboardComponent))]
[CanEditMultipleObjects]
public class AgentBlackboardComponentEditor : ComponentEditor
{

	protected override void DrawSettings()
	{
		AgentBlackboardComponent blackboardComponent = m_component as AgentBlackboardComponent;
		if (blackboardComponent != null && blackboardComponent.Editor_Settings != null)
		{
			;
		}
	}

}