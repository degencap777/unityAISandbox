using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumMaskFieldAttribute : PropertyAttribute
{
	
	public string fieldName = "";

	// -------------------------------------------------------------------------------------------------

	public EnumMaskFieldAttribute()
	{
	}

	// -------------------------------------------------------------------------------------------------

	public EnumMaskFieldAttribute(string name)
	{
		fieldName = name;
	}
	
}
