using UnityEngine;

public class AIDiagnosticCommand_Forget : AIDiagnosticCommand
{

	[SerializeField]
	private bool m_clearWorkingMemory = false;

	[SerializeField]
	private bool m_clearHistoricMemory = false;

	// --------------------------------------------------------------------------------

	protected override void Execute()
	{
		if (m_brain == null)
		{
			return;
		}

		if (m_clearWorkingMemory)
		{
			m_brain.ClearWorkingMemory();
		}

		if (m_clearHistoricMemory)
		{
			// #SteveD >>> once historic memory is in
			// #SteveD >>> change the name 'historic memory'
			//m_brain.ClearHistoricMemory();
		}
	}

}