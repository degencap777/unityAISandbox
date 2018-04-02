using UnityEngine;

namespace AISandbox.Health
{
	[CreateAssetMenu(fileName = "HealthConfig", menuName = "Component Config/Health", order = 1)]
	public class HealthConfig : ScriptableObject
	{

		[SerializeField]
		private float m_maxHealth = 100.0f;
		public float MaxHealth { get { return m_maxHealth; } }

		[SerializeField]
		private float m_damageScalar = 1.0f;
		public float DamageScalar { get { return m_damageScalar; } }

	}
}