using AISandbox.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace AISandbox.Container
{
	public class OctTree
	{

		private AreaBounds m_bounds = null;
		public AreaBounds Bounds { get { return m_bounds; } }

		private List<OctTreeOccupant> m_occupants = new List<OctTreeOccupant>();
		public List<OctTreeOccupant>.Enumerator OccupantEnumerator { get { return m_occupants.GetEnumerator(); } }

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

		public bool Insert(OctTreeOccupant occupant)
		{
			if (IsLeaf)
			{
				if (m_occupants.Contains(occupant) == false)
				{
					m_occupants.Add(occupant);
				}
				return true;
			}
			else
			{
				int inserted = 0;
				for (int i = 0; i < m_children.Count; ++i)
				{
					if (m_children[i].m_bounds.Contains(occupant))
					{
						inserted += m_children[i].Insert(occupant) ? 1 : 0;
					}
				}
				return inserted > 0;
			}
		}

		// ----------------------------------------------------------------------------

		public bool Remove(OctTreeOccupant occupant)
		{
			if (IsLeaf)
			{
				return m_occupants.Remove(occupant);
			}
			else
			{
				int removed = 0;
				for (int i = 0; i < m_children.Count; ++i)
				{
					if (m_children[i].m_bounds.Contains(occupant))
					{
						removed += m_children[i].Remove(occupant) ? 1 : 0;
					}
				}
				return removed > 0;
			}
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
				for (int t = m_occupants.Count - 1; t >= 0; --t)
				{
					if (m_children[c].m_bounds.Contains(m_occupants[t]))
					{
						m_children[c].Insert(m_occupants[t]);
						m_occupants.RemoveAt(t);
					}
				}
			}
			m_occupants.Clear();
		}

		// ----------------------------------------------------------------------------

		public void Combine()
		{
			for (int c = 0; c < m_children.Count; ++c)
			{
				m_children[c].Combine();
				m_occupants.AddRange(m_children[c].m_occupants);
			}
			m_children.Clear();
		}

		// ----------------------------------------------------------------------------

		public int GetOccupantCount()
		{
			if (IsLeaf)
			{
				return m_occupants.Count;
			}
			else
			{
				int count = 0;
				for (int c = 0; c < m_children.Count; ++c)
				{
					count += m_children[c].GetOccupantCount();
				}
				return count;
			}
		}

		// ----------------------------------------------------------------------------

		public void CaptureMigrants(List<OctTreeOccupant> migrants)
		{
			if (IsLeaf)
			{
				for (int t = m_occupants.Count - 1; t >= 0; --t)
				{
					if (migrants.Contains(m_occupants[t]))
					{
						continue;
					}
					else if (m_bounds.Contains(m_occupants[t]) == false)
					{
						migrants.Add(m_occupants[t]);
						m_occupants.Remove(m_occupants[t]);
					}
					else if (m_bounds.Intersects(m_occupants[t]))
					{
						migrants.Add(m_occupants[t]);
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