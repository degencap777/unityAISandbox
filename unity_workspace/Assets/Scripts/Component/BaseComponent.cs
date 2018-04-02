using UnityEngine;

namespace AISandbox.Component
{
	public abstract class BaseComponent : MonoBehaviour
	{

		public virtual void OnAwake() { }
		public virtual void OnStart() { }
		public virtual void OnUpdate() { }
		public virtual void Destroy() { }

	}
}