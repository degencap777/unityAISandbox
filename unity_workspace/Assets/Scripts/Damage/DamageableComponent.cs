using UnityEngine;

public class DamageableComponent : BaseComponent
{
	
	[SerializeField]
	private float m_maxHealth = 100.0f;

	[SerializeField]
	private Scalar m_damageScalar = null;

	// --------------------------------------------------------------------------------

	private float m_currentHealth = 100.0f;

	public delegate void HealthChanged(float newValue);
	public event HealthChanged OnHealthChanged;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		m_currentHealth = m_maxHealth;
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
		if (m_maxHealth <= 0.0f)
		{
			m_maxHealth = 1.0f;
		}

		if (m_currentHealth > m_maxHealth)
		{
			m_currentHealth = m_maxHealth;
		}
	}

	// --------------------------------------------------------------------------------

	public void Damage(float damage)
	{
		if (damage > 0.0f)
		{
			AlterHealth(-(m_damageScalar != null ? m_damageScalar.ScaleValue(damage) : damage));
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
		m_currentHealth = Mathf.Clamp(m_currentHealth + amount, 0.0f, m_maxHealth);

		if (previousHealth != m_currentHealth && OnHealthChanged != null)
		{
			OnHealthChanged(m_currentHealth);
		}
	}

}