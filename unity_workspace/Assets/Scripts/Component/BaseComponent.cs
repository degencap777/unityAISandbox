using UnityEngine;

public abstract class BaseComponent : MonoBehaviour
{

	public abstract void OnAwake();
	public abstract void OnStart();
	public abstract void OnUpdate();
	public abstract void Destroy();

}