using AISandbox.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AISandbox.Container
{
	public class OctTree
	{

		private AreaBounds m_bounds = null;
		public AreaBounds Bounds { get { return m_bounds; } }

		private List<Transform> m_transforms = new List<Transform>();
		public List<Transform>.Enumerator TransformEnumerator { get { return m_transforms.GetEnumerator(); } }

		private List<OctTree> m_children = new List<OctTree>(8);
		public List<OctTree>.Enumerator ChildEnumerator { get { return m_children.GetEnumerator(); } }

		private OctTree m_parent = null;
		public OctTree Parent { get { return m_parent; } }

		public bool IsRoot { get { return m_parent == null; } }
		public bool IsLeaf { get { return m_children.Count == 0; } }

		// ----------------------------------------------------------------------------

		public OctTree(OctTree parent, AreaBounds bounds)
		{
			m_parent = parent;
			m_bounds = bounds;
		}

		// ----------------------------------------------------------------------------

		public int GetDepth()
		{
			int depth = 1;
			OctTree current = this;

			while (current != null)
			{
				++depth;
				current = current.m_parent;
			}

			return depth;
		}

		// ----------------------------------------------------------------------------

		public bool Insert(Transform transform)
		{
			if (IsLeaf)
			{
				m_transforms.Add(transform);
			}
			else
			{
				for (int i = 0; i < m_children.Count; ++i)
				{
					if (m_children[i].m_bounds.Contains(transform.position))
					{
						m_children[i].Insert(transform);
						return true;
					}
				}
			}
			return false;
		}

		// ----------------------------------------------------------------------------

		public bool Remove(Transform transform)
		{
			if (IsLeaf)
			{
				return m_transforms.Remove(transform);
			}
			else
			{
				for (int i = 0; i < m_children.Count; ++i)
				{
					if (m_children[i].m_bounds.Contains(transform.position))
					{
						m_children[i].Remove(transform);
						return true;
					}
				}
			}
			return false;
		}

		// ----------------------------------------------------------------------------

		public bool Contains(Vector3 position)
		{
			return m_bounds.Contains(position);
		}

		// ----------------------------------------------------------------------------

		public void Split()
		{
			if (IsLeaf == false)
			{
				Debug.LogError("[OctTree::Split] failed, trying to split a non-leaf node");
				return;
			}

			Vector3 min = m_bounds.MinBounds;
			Vector3 max = m_bounds.MaxBounds;
			Vector3 halfSize = m_bounds.Size * 0.5f;

			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x, min.y, min.z), new Vector3(min.x + halfSize.x, min.y + halfSize.y, min.z + halfSize.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x + halfSize.x, min.y, min.z), new Vector3(max.x, min.y + halfSize.y, min.z + halfSize.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x, min.y, min.z + halfSize.z), new Vector3(min.x + halfSize.x, min.y + halfSize.y, max.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x + halfSize.x, min.y, min.z + halfSize.z), new Vector3(max.x, min.y + halfSize.y, max.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x, min.y + halfSize.y, min.z), new Vector3(min.x + halfSize.x, max.y, min.z + halfSize.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x + halfSize.x, min.y + halfSize.y, min.z), new Vector3(max.x, max.y, min.z + halfSize.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x, min.y + halfSize.y, min.z + halfSize.z), new Vector3(min.x + halfSize.x, max.y, max.z))));
			m_children.Add(new OctTree(this, new AreaBounds(new Vector3(min.x + halfSize.x, min.y + halfSize.y, min.z + halfSize.z),	new Vector3(max.x, max.y, max.z))));

			for (int c = 0; c < m_children.Count; ++c)
			{
				for (int t = m_transforms.Count - 1; t >= 0; --t)
				{
					if (m_children[c].m_bounds.Contains(m_transforms[t].position))
					{
						m_children[c].Insert(m_transforms[t]);
						m_transforms.RemoveAt(t);
					}
				}
			}
			m_transforms.Clear();
		}

		// ----------------------------------------------------------------------------

		public void Combine()
		{
			for (int c = 0; c < m_children.Count; ++c)
			{
				m_children[c].Combine();
				m_transforms.AddRange(m_children[c].m_transforms);
			}
			m_children.Clear();
		}

		// ----------------------------------------------------------------------------

		public int GetTransformCount()
		{
			if (IsLeaf)
			{
				return m_transforms.Count;
			}
			else
			{
				int count = 0;
				for (int c = 0; c < m_children.Count; ++c)
				{
					count += m_children[c].GetTransformCount();
				}
				return count;
			}
		}

		// ----------------------------------------------------------------------------

		public void CaptureMigrants(List<Transform> migrants)
		{
			if (IsLeaf)
			{
				for (int t = m_transforms.Count - 1; t >= 0; --t)
				{
					if (Contains(m_transforms[t].position) == false)
					{
						migrants.Add(m_transforms[t]);
						m_transforms.RemoveAt(t);
					}
				}
			}
			else
			{
				for (int c = 0; c < m_children.Count; ++c)
				{
					m_children[c].CaptureMigrants(migrants);
				}
			}
		}

		// ----------------------------------------------------------------------------

		public void GetAllLeafNodes(List<OctTree> leafNodes)
		{
			if (IsLeaf)
			{
				leafNodes.Add(this);
			}
			else
			{
				for (int c = 0; c < m_children.Count; ++c)
				{
					m_children[c].GetAllLeafNodes(leafNodes);
				}
			}
		}

		// ----------------------------------------------------------------------------

		public void GetAllParentNodes(List<OctTree> parentNodes)
		{
			if (IsLeaf == false)
			{
				parentNodes.Add(this);
				for (int c = 0; c < m_children.Count; ++c)
				{
					m_children[c].GetAllParentNodes(parentNodes);
				}
			}
		}

		// ----------------------------------------------------------------------------
		// Editor specific ------------------------------------------------------------

#if UNITY_EDITOR

		public void GizmoDrawBounds()
		{
			Gizmos.DrawWireCube(m_bounds.MinBounds + (m_bounds.Size * 0.5f), m_bounds.Size);
			
			for (int i = 0; i < m_children.Count; ++i)
			{
				m_children[i].GizmoDrawBounds();
			}
		}

#endif // UNITY_EDITOR

	}
}