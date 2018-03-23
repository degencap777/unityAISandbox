using UnityEngine;

public class DamageableComponent : BaseComponent
{

	[SerializeField]
	private DamageableSettings m_settings = null;

	// --------------------------------------------------------------------------------

	private float m_currentHealth = 100.0f;

	public delegate void HealthChanged(float newValue);
	public event HealthChanged OnHealthChanged;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_settings == null)
		{
			m_settings = ScriptableObject.CreateInstance<DamageableSettings>();
		}

		m_currentHealth = m_settings.MaxHealth;
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

	protected virtual void OnValidate()
	{
		if (m_currentHealth > m_settings.MaxHealth)
		{
			m_currentHealth = m_settings.MaxHealth;
		}
	}

	// --------------------------------------------------------------------------------

	public void Damage(float damage)
	{
		if (damage > 0.0f)
		{
			AlterHealth(m_settings.DamageScalar * damage);
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
		float previousHealth = m_currentHealth;
		m_currentHealth = Mathf.Clamp(m_currentHealth + amount, 0.0f, m_settings.MaxHealth);

		if (previousHealth != m_currentHealth && OnHealthChanged != null)
		{
			OnHealthChanged(m_currentHealth);
		}
	}

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public DamageableSettings Editor_Settings { get { return m_settings; } }
	
#endif // UNITY_EDITOR


}