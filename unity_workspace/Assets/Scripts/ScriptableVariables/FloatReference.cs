using System;
using UnityEngine;

[Serializable]
public class FloatReference
{

	// #SteveD	>>> property drawer:
	// - toggle constant/variable bool using VariableReferenceType enum dropdown
	// - single line
	// - hide whichever property isn't being used

	[SerializeField]
	private bool m_useConstant = true;

	[SerializeField]
	private float m_constant = 0.0f;

	[SerializeField]
	private FloatVariable m_variable = null;

	// --------------------------------------------------------------------------------

	public float Value { get { return m_useConstant ? m_constant : m_variable.Value; } }

}