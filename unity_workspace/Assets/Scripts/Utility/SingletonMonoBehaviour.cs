using UnityEngine;

public abstract class SingletonMonoBehaviourBase : MonoBehaviour
{
	protected abstract void Awake();
}

// ------------------------------------------------------------------------------------

public abstract class SingletonMonoBehaviour<T> : SingletonMonoBehaviourBase where T : SingletonMonoBehaviour<T>
{

	private static T m_instance = null;
	public static T Instance { get { return m_instance; } }

	// --------------------------------------------------------------------------------

	protected sealed override void Awake()
	{
		if (m_instance == null)
		{
			m_instance = GetComponent<T>();
			Debug.Assert(m_instance != null, "[SingletonMonoBehaviour::Awake] GetComponent<T> failed\n");

			OnAwake();
		}
		else
		{
			DestroyImmediate(GetComponent<T>());
		}
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnAwake()
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnDestroy()
	{
		if (m_instance == GetComponent<T>())
		{
			m_instance = null;
		}
	}

}
