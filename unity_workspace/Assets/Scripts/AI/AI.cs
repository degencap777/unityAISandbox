using UnityEngine;

public class AI : MonoBehaviour
{

	[SerializeField]
	private Agent m_owner = null;

	[SerializeField]
	private Memory m_memory = new Memory();

	[SerializeField]
	private BehaviourCollection m_behaviours = new BehaviourCollection();

	// --------------------------------------------------------------------------------

	protected virtual void Awake()
	{
		m_memory.Initialise();
		m_behaviours.Initialise(m_owner, m_memory);
	}

	// --------------------------------------------------------------------------------

	protected virtual void Update()
	{
		m_behaviours.OnUpdate();
	}

}
