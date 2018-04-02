using AISandbox.AI;
using UnityEditor;

[CustomEditor(typeof(AgentBlackboardComponent))]
[CanEditMultipleObjects]
public class AgentBlackboardComponentEditor : ComponentEditor
{

	protected override void DrawConfig()
	{
		AgentBlackboardComponent blackboardComponent = m_component as AgentBlackboardComponent;
		if (blackboardComponent != null && blackboardComponent.Editor_Config != null)
		{
			;
		}
	}

}