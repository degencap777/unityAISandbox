using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
public class Perception_Visual : Perception
{

	// #SteveD >>> Implement OnTriggerEnter, OnTriggerExit

	// #SteveD >>> Track all Agents in view collider. Add on enter, remove on exit

	// #SteveD >>> CanPercieve:
	//				>>> If we're concerned with an agent, check that they're in our trigger list first
	//				>>> ... then cast a ray to them
	//				>>> If we're concerned with a location (ie. trigger has no Actor), just cast a ray

	[SerializeField]
	private float m_horizontalFieldOfView = 60.0f;

	[SerializeField]
	private float m_verticalFieldOfView = 15.0f;

	[SerializeField]
	private float m_visionRange = 10.0f;

	// --------------------------------------------------------------------------------

	private MeshCollider m_meshCollider = null;
	private MeshFilter m_meshFilter = null;

	// --------------------------------------------------------------------------------

	public override PerceptionType PerceptionType { get { return PerceptionType.Visual; } }

	// --------------------------------------------------------------------------------

	protected override void OnAwake()
	{
		base.OnAwake();

		m_meshCollider = GetComponent<MeshCollider>();
		m_meshFilter = GetComponent<MeshFilter>();

		if (m_meshCollider != null && m_meshFilter != null)
		{
			// #SteveD	>>> construct mesh based on m_horizontalFieldOfView, m_verticalFieldOfView and m_visionRange
			
			Vector3[] vertices = {
				new Vector3 (0.0f, 0.0f, 0.0f), // near bottom left
				new Vector3 (2.0f, 0.0f, 0.0f), // near bottom right
				new Vector3 (3.0f, 3.0f, 0.0f), // near top right
				new Vector3 (0.0f, 4.0f, 0.0f), // near top left
				new Vector3 (0.0f, 5.0f, 5.0f), // far top left
				new Vector3 (6.0f, 6.0f, 6.0f), // far top right
				new Vector3 (7.0f, 0.0f, 7.0f), // far bottom right
				new Vector3 (0.0f, 0.0f, 8.0f), // far bottom left
			};

			int[] triangles = {
				0, 2, 1,	0, 3, 2, // front
				2, 3, 4,	2, 4, 5, // top
				1, 2, 5,	1, 5, 6, // right
				0, 7, 4,	0, 4, 3, // left
				5, 4, 7,	5, 7, 6, // back
				0, 6, 7,	0, 1, 6, // bottom
			};

			Mesh mesh = m_meshFilter.mesh;
			mesh.Clear();
			mesh.vertices = vertices;
			mesh.triangles = triangles;
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
