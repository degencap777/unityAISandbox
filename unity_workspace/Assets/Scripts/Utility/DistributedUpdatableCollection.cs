using System;
using System.Collections.Generic;

[Serializable]
public class UpdatableCollection<T>
{

	public List<T> m_updatables = new List<T>();
	public float m_updateTime = 0.0f;
	
}
