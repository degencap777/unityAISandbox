using UnityEngine;

[CreateAssetMenu(fileName = "FloatScriptable", menuName = "Scriptable Variable/Float", order = 1)]
public class FloatScriptable : ScriptableObject
{
	
	[SerializeField]
	private float m_value = 0.0f;
	
	public float Value
	{
		get { return m_value; }
		set { m_value = value; }
	}

}