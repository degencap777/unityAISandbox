using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshCollider))]
public class Perception_Visual : Perception
{

	private static readonly int k_maxViewTriggerSegmentAngle = 30;

	// #SteveD >>> Implement OnTriggerEnter, OnTriggerExit

	// #SteveD >>> Track all Agents in view collider. Add on enter, remove on exit

	// #SteveD >>> CanPercieve:
	//				>>> If we're concerned with an agent, check that they're in our trigger list first
	//				>>> ... then cast a ray to them
	//				>>> If we're concerned with a location (ie. trigger has no Actor), just cast a ray

	// #SteveD >>> once created using horizontal FOV, use vertical FOV too

	[SerializeField]
	private float m_horizontalFieldOfView = 60.0f;

	//[SerializeField]
	//private float m_verticalFieldOfView = 15.0f;

	[SerializeField, Range(1.0f, 180.0f)]
	private float m_visionRange = 10.0f;

	// --------------------------------------------------------------------------------

	private Transform m_transform = null;
	private MeshCollider m_meshCollider = null;

	// --------------------------------------------------------------------------------

	public override PerceptionType PerceptionType { get { return PerceptionType.Visual; } }

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		base.OnAwake();

		m_transform = GetComponent<Transform>();
		m_meshCollider = GetComponent<MeshCollider>();
		
		BuildVisionTriggerMesh();
	}

	// --------------------------------------------------------------------------------

	private void BuildVisionTriggerMesh()
	{
		if (m_meshCollider != null && m_transform != null)
		{
			// #SteveD	>>> construct mesh based on m_horizontalFieldOfView, m_verticalFieldOfView and m_visionRange
			//			>>> 30 - 45 degree segments!

			// number of horizontal segments
			int horizontalSegmentCount = (int)m_horizontalFieldOfView / k_maxViewTriggerSegmentAngle;
			horizontalSegmentCount += (int)m_horizontalFieldOfView % k_maxViewTriggerSegmentAngle > 0 ? 1 : 0;

			// angle between horizontal segments
			float segmentAngle = m_horizontalFieldOfView / horizontalSegmentCount;

			List<Vector3> vertices = new List<Vector3>() {
				new Vector3 (0.0f, 0.0f, 0.0f),					// near bottom
				new Vector3 (0.0f, transform.position.y, 0.0f), // near top
			};

			Vector3 leftEdge = Quaternion.Euler(0.0f, -m_horizontalFieldOfView * 0.5f, 0.0f) * 
				new Vector3(0.0f, 0.0f, m_visionRange);
			
			for (int i = 0; i <= horizontalSegmentCount; ++i)
			{
				vertices.Add(leftEdge + new Vector3(0.0f, m_transform.position.y, 0.0f));
				vertices.Add(leftEdge);
				leftEdge = Quaternion.Euler(0.0f, segmentAngle, 0.0f) * leftEdge;
			}

			// clockwise winding order

			// add left side quad
			List<int> triangles = new List<int>() {
				0, 1, 2,				// left
				0, 2, 3,				// left
			};

			// #SteveD >>> top triangle per segment
			// #SteveD >>> bottom triangle per segment
			// #SteveD >>> end quad per segment

			int vertexCount = vertices.Count - 1;
			// add right side quad
			triangles.AddRange(
				new int[] { 
					0, 1, vertexCount - 1,
					0, 4, vertexCount 
				}
			);
			
			// get or create mesh
			Mesh mesh = m_meshCollider.sharedMesh;
			if (mesh == null)
			{
				mesh = new Mesh();
			}

			// set mesh data
			mesh.Clear();
			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();
			mesh.RecalculateBounds(); // #SteveD >>> is this required? research
			m_meshCollider.sharedMesh = mesh;
		}
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

	protected override bool CanPercieve(PerceptionTrigger trigger)
	{
		return false;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnTriggerEnter(Collider collider)
	{
		;
	}

	// --------------------------------------------------------------------------------

	protected virtual void OnTriggerExit(Collider collider)
	{
		;
	}

}
