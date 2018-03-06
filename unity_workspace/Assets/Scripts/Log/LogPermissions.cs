using System;
using UnityEngine;

[Serializable]
public class LogPermissions
{

	[SerializeField]
	private string m_tag = string.Empty;
	public string Tag { get { return m_tag; } }

	[SerializeField]
	private bool m_info = true;
	public bool Info { get { return m_info; } }

	[SerializeField]
	private bool m_warning = true;
	public bool Warning { get { return m_warning; } }

	[SerializeField]
	private bool m_error = true;
	public bool Error { get { return m_error; } }

	// --------------------------------------------------------------------------------

	public LogPermissions(string tag, bool info, bool warning, bool error)
	{
		m_tag = tag;
		m_info = info;
		m_warning = warning;
		m_error = error;
	}

}
