using UnityEngine;

public class SharedBlackboard : SingletonMonoBehaviour<SharedBlackboard>
{

	[SerializeField]
	private BSP m_bsp = null;
	public BSP BSP { get { return m_bsp; } }

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

}