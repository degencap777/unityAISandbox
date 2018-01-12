using System;
using UnityEngine;

[Serializable]
public class Memory
{

	[SerializeField]
	private WorkingMemory m_workingMemory = new WorkingMemory();
	public WorkingMemory WorkingMemory { get { return m_workingMemory; } }

	// --------------------------------------------------------------------------------

	public void Initialise()
	{
		
	}
	
}
