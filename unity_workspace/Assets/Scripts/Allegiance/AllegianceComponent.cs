using UnityEngine;

public class AllegianceComponent : BaseComponent
{

	private static readonly string k_shaderColourProperty = "_Color";

	// --------------------------------------------------------------------------------
	
	[SerializeField]
	private AllegianceConfig m_config = null;

	// --------------------------------------------------------------------------------

	private Renderer m_renderer = null;
	private int m_shaderColourId = -1;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<AllegianceConfig>();
		}

		m_renderer = GetComponentInChildren<MeshRenderer>();
		Debug.Assert(m_renderer != null, "AllegianceComponent::GetComponentInChildren<MeshRenderer> failed\n");

		m_shaderColourId = Shader.PropertyToID(k_shaderColourProperty);
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		SetAllegianceColour();
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

	private void SetAllegianceColour()
	{
		Color colour = m_config != null ? m_config.Colour : Color.grey;
		m_renderer.material.SetColor(m_shaderColourId, colour);
	}
	
	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public AllegianceConfig Editor_Config { get { return m_config; } }

#endif // UNITY_EDITOR

}