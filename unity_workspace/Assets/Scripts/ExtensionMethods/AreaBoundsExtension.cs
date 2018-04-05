using AISandbox.Container;
using AISandbox.Utility;
using UnityEngine;

public static class AreaBoundsExtension
{

	public static bool Contains(this AreaBounds bounds, OctTreeOccupant octTreeOccupant)
	{
		if (octTreeOccupant == null)
		{
			return false;
		}

		Transform sphereTransform = octTreeOccupant.Transform;
		SphereCollider sphere = octTreeOccupant.SphereCollider;
		if (sphereTransform == null || sphere == null)
		{
			return false;
		}

		Vector3 spherePosition = sphereTransform.position;
		spherePosition += sphere.center;
		float sphereRadius = sphere.radius;

		if (spherePosition.x - sphereRadius > bounds.MaxBounds.x ||
			spherePosition.y - sphereRadius > bounds.MaxBounds.y ||
			spherePosition.z - sphereRadius > bounds.MaxBounds.z ||
			spherePosition.x + sphereRadius < bounds.MinBounds.x ||
			spherePosition.y + sphereRadius < bounds.MinBounds.y ||
			spherePosition.z + sphereRadius < bounds.MinBounds.z)
		{
			return false;
		}
		return true;
	}

	// --------------------------------------------------------------------------------

	public static bool Intersects(this AreaBounds bounds, OctTreeOccupant octTreeOccupant)
	{
		if (octTreeOccupant == null)
		{
			return false;
		}

		Transform sphereTransform = octTreeOccupant.Transform;
		SphereCollider sphere = octTreeOccupant.SphereCollider;
		if (sphereTransform == null || sphere == null)
		{
			return false;
		}

		Vector3 spherePosition = sphereTransform.position;
		spherePosition += sphere.center;
		float sphereRadius = sphere.radius;

		return ((spherePosition.x - sphereRadius < bounds.MinBounds.x && spherePosition.x + sphereRadius > bounds.MinBounds.x) ||
			(spherePosition.y - sphereRadius < bounds.MinBounds.y && spherePosition.y + sphereRadius > bounds.MinBounds.y) ||
			(spherePosition.z - sphereRadius < bounds.MinBounds.z && spherePosition.z + sphereRadius > bounds.MinBounds.z) ||
			(spherePosition.x - sphereRadius < bounds.MaxBounds.x && spherePosition.x + sphereRadius > bounds.MaxBounds.x) ||
			(spherePosition.y - sphereRadius < bounds.MaxBounds.y && spherePosition.y + sphereRadius > bounds.MaxBounds.y) ||
			(spherePosition.z - sphereRadius < bounds.MaxBounds.z && spherePosition.z + sphereRadius > bounds.MaxBounds.z));
	}

}