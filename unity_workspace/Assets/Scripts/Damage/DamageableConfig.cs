using UnityEngine;

[CreateAssetMenu(fileName = "DamageableConfig", menuName = "Component Config/Damageable", order = 1)]
public class DamageableConfig : ScriptableObject
{

	[SerializeField]
	private float m_maxHealth = 100.0f;
	public float MaxHealth { get { return m_maxHealth; } }

	[SerializeField]
	private float m_damageScalar = 1.0f;
	public float DamageScalar { get { return m_damageScalar; } }

}