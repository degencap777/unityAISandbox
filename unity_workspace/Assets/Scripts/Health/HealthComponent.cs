using UnityEngine;

public class HealthComponent : BaseComponent
{

	[SerializeField]
	private HealthConfig m_config = null;

	// --------------------------------------------------------------------------------

	[SerializeField]
	private FloatReference m_currentHealth = null;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<HealthConfig>();
		}

		m_currentHealth.Value = m_config.MaxHealth;
	}

	// --------------------------------------------------------------------------------

	public void Damage(float damage)
	{
		if (damage > 0.0f)
		{
			AlterHealth(m_config.DamageScalar * -damage);
		}
	}

	// --------------------------------------------------------------------------------

	public void Heal(float health)
	{
		if (health > 0.0f)
		{
			AlterHealth(health);
		}
	}

	// --------------------------------------------------------------------------------

	private void AlterHealth(float amount)
	{
		m_currentHealth.Value = Mathf.Clamp(m_currentHealth.Value + amount, 0.0f, m_config.MaxHealth);
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public HealthConfig Editor_Config { get { return m_config; } }

	// --------------------------------------------------------------------------------

	protected virtual void OnValidate()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<HealthConfig>();
		}

		if (m_currentHealth.Value > m_config.MaxHealth)
		{
			m_currentHealth.Value = m_config.MaxHealth;
		}
	}

#endif // UNITY_EDITOR


}