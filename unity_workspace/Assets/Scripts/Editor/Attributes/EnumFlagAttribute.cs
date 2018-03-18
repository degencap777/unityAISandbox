using UnityEngine;

public class EnumFlagAttribute : PropertyAttribute
{

	public string m_enumName;

	// --------------------------------------------------------------------------------

	public EnumFlagAttribute() 
	{
		;
	}

	// --------------------------------------------------------------------------------

	public EnumFlagAttribute(string name)
	{
		m_enumName = name;
	}

}