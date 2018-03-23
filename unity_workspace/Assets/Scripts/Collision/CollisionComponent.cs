using UnityEngine;

public class CollisionComponent : BaseComponent
{

	[SerializeField]
	private CollisionSettings m_settings = null;

	// --------------------------------------------------------------------------------
	
	private Collider m_collider = null;

	// --------------------------------------------------------------------------------

	public override void OnAwake() 
	{
		if (m_settings == null)
		{
			m_settings = ScriptableObject.CreateInstance<CollisionSettings>();
		}

		m_collider = GetComponent<Collider>();
		Debug.Assert(m_collider != null, "[Collisioncomponent::OnAwake] GetComponent<Collider> failed\n");
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

	// Editor specific ----------------------------------------------------------------
	// --------------------------------------------------------------------------------

#if UNITY_EDITOR

	public CollisionSettings Editor_Settings { get { return m_settings; } }

#endif // UNITY_EDITOR

}