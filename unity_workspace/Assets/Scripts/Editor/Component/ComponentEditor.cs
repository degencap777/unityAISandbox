using UnityEditor;

public abstract class ComponentEditor : Editor
{

	protected BaseComponent m_component = null;
	private bool foldout = false;

	// --------------------------------------------------------------------------------

	protected virtual void OnEnable()
	{
		m_component = target as BaseComponent;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawDefaultInspector();
		
		if (m_component != null)
		{
			EditorGUILayout.Space();

			foldout = EditorGUILayout.Foldout(foldout, "Settings");
			if (foldout)
			{
				EditorGUI.indentLevel += 1;
				EditorGUI.BeginDisabledGroup(true);
				DrawSettings();
				EditorGUI.EndDisabledGroup();
				EditorGUI.indentLevel -= 1;
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

	// --------------------------------------------------------------------------------

	protected abstract void DrawSettings();

}