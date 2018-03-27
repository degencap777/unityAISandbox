using UnityEngine;

public class CollisionComponent : BaseComponent
{

	[SerializeField]
	private CollisionConfig m_config = null;

	// --------------------------------------------------------------------------------
	
	private Collider m_collider = null;

	// --------------------------------------------------------------------------------

	public override void OnAwake() 
	{
		if (m_config == null)
		{
			m_config = ScriptableObject.CreateInstance<CollisionConfig>();
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

	public CollisionConfig Editor_Config { get { return m_config; } }

#endif // UNITY_EDITOR

}