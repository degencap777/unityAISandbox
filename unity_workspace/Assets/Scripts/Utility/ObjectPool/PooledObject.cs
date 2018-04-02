

namespace AISandbox.Utility
{
	public interface IPooledObject
	{
		void ReleaseResources();
		void Reset();
	}
}