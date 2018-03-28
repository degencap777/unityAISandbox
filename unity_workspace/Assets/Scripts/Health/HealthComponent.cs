using UnityEngine;

public class HealthComponent : BaseComponent
{

	// #SteveD	>>> use FloatReference to enable sharing m_currentHealth

	[SerializeField]
	private HealthConfig m_config = null;

	// --------------------------------------------------------------------------------

	private float m_currentHealth = 0.0f;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<HealthConfig>();
		}

		m_currentHealth = m_config.MaxHealth;
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public override void Destroy()
	{
		;
	}

	// --------------------------------------------------------------------------------

	public void Damage(float damage)
	{
		if (damage > 0.0f)
		{
			AlterHealth(m_config.DamageScalar * damage);
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
		m_currentHealth = Mathf.Clamp(m_currentHealth + amount, 0.0f, m_config.MaxHealth);
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

		if (m_currentHealth > m_config.MaxHealth)
		{
			m_currentHealth = m_config.MaxHealth;
		}
	}

#endif // UNITY_EDITOR


}