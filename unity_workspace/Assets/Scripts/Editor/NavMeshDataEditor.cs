using AISandbox.Navigation;
using UnityEditor;

[CustomEditor(typeof(NavMeshData))]
public class NavMeshDataEditor : Editor
{

	private NavMeshData m_navmeshData = null;

	// --------------------------------------------------------------------------------

	protected virtual void OnEnable()
	{
		m_navmeshData = target as NavMeshData;
	}

	// --------------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		if (m_navmeshData != null)
		{
			EditorGUILayout.LabelField(string.Format("{0} nodes", m_navmeshData.NodeDataCount));
		}
		serializedObject.ApplyModifiedProperties();
	}
	
}