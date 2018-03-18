using UnityEngine;

public class AllegianceComponent : BaseComponent
{

	private static readonly string k_shaderColourProperty = "_Color";

	// --------------------------------------------------------------------------------

	[SerializeField]
	private string m_allegianceName = Allegiance.k_noAllegianceName;
	private Allegiance m_allegiance = null;

	// --------------------------------------------------------------------------------

	private Renderer m_renderer = null;
	private int m_shaderColourId = -1;

	// --------------------------------------------------------------------------------

	public override void OnAwake()
	{
		m_renderer = GetComponentInChildren<MeshRenderer>();
		Debug.Assert(m_renderer != null, "AllegianceComponent::GetComponentInChildren<MeshRenderer> failed\n");

		m_shaderColourId = Shader.PropertyToID(k_shaderColourProperty);
	}

	// --------------------------------------------------------------------------------

	public override void OnStart()
	{
		AllegianceManager allegianceManager = AllegianceManager.Instance;
		if (allegianceManager != null)
		{
			m_allegiance = allegianceManager.GetAllegiance(m_allegianceName);
		}

		SetAllegianceColour();
	}

	// --------------------------------------------------------------------------------

	public override void OnUpdate()
	{
		;
	}

	// --------------------------------------------------------------------------------

	private void SetAllegianceColour()
	{
		if (m_allegiance != null)
		{
			m_renderer.material.SetColor(m_shaderColourId, m_allegiance.Colour);
		}
		else
		{
			m_renderer.material.SetColor(m_shaderColourId, Color.grey);
		}
	}

	// --------------------------------------------------------------------------------
	
	public void ChangeAllegiance(Allegiance allegiance)
	{
		m_allegiance = allegiance;
		SetAllegianceColour();
	}
	
}