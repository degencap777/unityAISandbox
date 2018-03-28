using UnityEngine;

public class SharedBlackboard : MonoBehaviour
{

	[SerializeField]
	private BSP m_bsp = null;
	public BSP BSP { get { return m_bsp; } }

}