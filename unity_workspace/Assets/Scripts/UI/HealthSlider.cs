using AISandbox.Health;
using AISandbox.Variable;
using UnityEngine;
using UnityEngine.UI;

namespace AISandbox.UI
{
	public class HealthSlider : MonoBehaviour
	{

		[SerializeField]
		private HealthConfig m_healthConfig = null;

		[SerializeField]
		private FloatVariable m_healthValue = null;

		// --------------------------------------------------------------------------------

		private Slider m_slider = null;
		private float m_previousValue = -1.0f;

		// --------------------------------------------------------------------------------

		protected virtual void Awake()
		{
			m_slider = GetComponentInChildren<Slider>();
			Debug.Assert(m_slider != null, "[HealthSlider::Awake] GetComponentInChildren<Slider> failed\n");

			Debug.Assert(m_healthConfig != null, "[HealthSlider::Awake] m_healthConfig is null\n");
			Debug.Assert(m_healthValue != null, "[HealthSlider::Awake] m_healthValue is null\n");

			if (m_healthConfig != null)
			{
				Debug.AssertFormat(m_healthConfig.MaxHealth > 0, "[HealthSlider::Awake] m_healthValue.MaxHealth is invalid: {0}\n", m_healthConfig.MaxHealth);
			}
		}

		// --------------------------------------------------------------------------------

		protected virtual void Update()
		{
			if (m_slider == null || m_healthConfig == null || m_healthValue == null)
			{
				return;
			}

			if (m_healthConfig.MaxHealth <= 0.0f)
			{
				return;
			}

			if (m_previousValue != m_healthValue.Value)
			{
				m_slider.value = Mathf.Clamp(m_healthValue.Value / m_healthConfig.MaxHealth, 0.0f, 1.0f);
				m_previousValue = m_healthValue.Value;
			}
		}

	}
}