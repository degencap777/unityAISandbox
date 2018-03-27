using UnityEngine;

public class DamageableComponent : BaseComponent
{

	[SerializeField]
	private DamageableConfig m_config = null;

	// --------------------------------------------------------------------------------

	// #SteveD	>>> m_currentHealth to use FloatReference. Player uses FloatVariable, Agent uses constant
	// #SteveD	>>> PlayerHealthUI can then reference PlayerHealth FloatReference
	private float m_currentHealth = 100.0f;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<DamageableConfig>();
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

	public DamageableConfig Editor_Config { get { return m_config; } }

	// --------------------------------------------------------------------------------

	protected virtual void OnValidate()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<DamageableConfig>();
		}

		if (m_currentHealth > m_config.MaxHealth)
		{
			m_currentHealth = m_config.MaxHealth;
		}
	}

#endif // UNITY_EDITOR


}