using UnityEngine;

namespace AISandbox.Variable
{
	[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variable/Float", order = 1)]
	public class FloatVariable : ScriptableObject
	{

		[SerializeField]
		private float m_value = 0.0f;

		public float Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

	}
}